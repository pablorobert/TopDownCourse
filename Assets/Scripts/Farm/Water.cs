using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public bool isDetectingPlayer;

    private PlayerController playerController;
    private PlayerItems playerItems;

    public int waterAmount = 5;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerItems = FindObjectOfType<PlayerItems>();
    }

    void Update()
    {

        if (isDetectingPlayer && playerController.IsActing)
        {
            playerController.IsActing = false;
            playerItems.addWater(waterAmount);
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
