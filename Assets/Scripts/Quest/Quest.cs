using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public int index;

    public bool isActive;
    public bool isCompleted;

    [TextArea] public string description;

    protected GameObject player;
    protected PlayerItems playerItems;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerItems = player.GetComponent<PlayerItems>();
    }

    public virtual void Check()
    {
        if (isCompleted) return;
    }

    public abstract void OnComplete();
}
