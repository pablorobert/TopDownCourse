using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    private bool isDetectingPlayer;

    private PlayerController playerController;
    private PlayerItems playerItems;

    private PlayerAnimation playerAnim;

    public int chance;

    public Transform fishLocation;

    public GameObject fish;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerItems = playerController.GetComponent<PlayerItems>();
        playerAnim = playerController.GetComponent<PlayerAnimation>();
    }

    void Update()
    {

        if (isDetectingPlayer && playerController.IsActing)
        {
            playerController.IsActing = false;
            playerAnim.OnCast();
        }
    }

    public void OnCatch()
    {
        int rand = Random.Range(0, 100);
        if (rand <= chance)
        {
            Instantiate(
                fish,
                fishLocation.position + new Vector3(Random.Range(-2f, 2f), 0f, 0f),
                transform.rotation
            );
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
