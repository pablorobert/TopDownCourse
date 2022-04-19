using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerItems playerItems = collider.GetComponent<PlayerItems>();
            if (!playerItems.IsFishFull())
            {
                playerItems.AddFish(1);
                Destroy(gameObject);
            }
        }
    }
}
