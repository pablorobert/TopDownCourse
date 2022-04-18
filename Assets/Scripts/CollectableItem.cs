using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
    public UnityEvent OnCollect;
    private PlayerController playerController;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && playerController.IsActing)
        {
            OnCollect?.Invoke();
        }
    }
}
