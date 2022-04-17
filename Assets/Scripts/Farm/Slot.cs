using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    private bool isDig;
    public int digAmount;
    private int originalDigAmount;

    public Sprite hole;

    public Sprite carrot;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalDigAmount = digAmount;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnHit()
    {
        digAmount--;
        if (digAmount < originalDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
        }
        else if (digAmount <= 0)
        {
            digAmount = 0;
            spriteRenderer.sprite = carrot;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isDig && collider.CompareTag("Shovel"))
        {
            OnHit();
        }
    }
}
