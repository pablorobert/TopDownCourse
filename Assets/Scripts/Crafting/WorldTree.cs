using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTree : MonoBehaviour
{
    public int health = 5;
    public ParticleSystem leafs;
    private int totalWood;
    private Animator anim;

    private bool isCut;
    public GameObject drop;
    void Start()
    {
        anim = GetComponent<Animator>();
        totalWood = Random.Range(1, 4);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isCut && collider.CompareTag("Axe"))
        {
            OnHit();
        }
    }

    void OnHit()
    {
        health--;
        leafs.Play();
        anim.SetTrigger("isHit");
        if (health <= 0)
        {
            anim.SetTrigger("cut");
            isCut = true;
            for (int i = 0; i < totalWood; i++)
            {
                Instantiate(drop, transform.position +
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f),
                transform.rotation);
            }
        }
    }
}
