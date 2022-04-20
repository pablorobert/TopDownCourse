using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public SpriteRenderer houseSprite;
    public Color inactiveColor;
    public Color activeColor;

    public Transform buildingPoint;

    public int woodAmount;

    public GameObject houseCollider;

    public float timeAmount;
    private float timeCount;
    private PlayerController playerController;
    private PlayerItems playerItems;
    private PlayerAnimation playerAnimation;

    private bool isDetectingPlayer;

    private bool isBuilt;

    private bool started;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerAnimation = playerController.GetComponent<PlayerAnimation>();
        playerItems = playerController.GetComponent<PlayerItems>();
    }

    void Update()
    {
        if (isBuilt)
            return;
        if (isDetectingPlayer && playerController.IsActing && !started
        && playerItems.currentWood >= woodAmount)
        {
            houseSprite.gameObject.SetActive(true);
            playerItems.currentWood -= woodAmount;
            playerController.transform.position = buildingPoint.position;
            playerController.IsActing = false;
            started = true;
            playerAnimation.OnHammer(started);
            houseSprite.color = inactiveColor;
        }

        if (started)
        {
            timeCount += Time.deltaTime;
            if (timeCount > timeAmount)
            {
                houseSprite.color = activeColor;
                houseCollider.gameObject.SetActive(true);
                playerAnimation.OnHammer(false);
                isBuilt = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isDetectingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isDetectingPlayer = false;
        }
    }
}
