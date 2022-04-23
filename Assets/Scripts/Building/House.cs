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

    private bool isStarted;

    private bool isAnimating;

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
        if (isDetectingPlayer && playerController.IsActing && !isStarted
        && playerItems.currentWood >= woodAmount && !isAnimating)
        {
            isAnimating = true;
            playerItems.currentWood -= woodAmount;

            StartCoroutine(BuildHouse());
        }

        if (isStarted)
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

    IEnumerator BuildHouse()
    {
        playerController.IsRunning = true;
        playerAnimation.OnRun();
        //look at the point
        playerController.FaceGameObject(buildingPoint.transform);

        while (Vector2.Distance(playerController.transform.position, buildingPoint.position) > 0.1f)
        {
            playerController.MoveToPosition(buildingPoint.position);
            yield return new WaitForSeconds(0.01f);
        }
        playerController.IsRunning = false;
        playerController.IsActing = false;
        //look at the house
        playerController.FaceGameObject(transform);
        //playerController.transform.position = buildingPoint.position;

        playerAnimation.OnHammer(true);
        houseSprite.gameObject.SetActive(true);
        isStarted = true;
        houseSprite.color = inactiveColor;

    }
}
