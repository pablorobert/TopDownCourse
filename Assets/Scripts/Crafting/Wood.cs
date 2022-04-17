using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float speed = -5f;
    public float timeMove = 0.3f;

    private float countTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime < timeMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerItems>().totalWood++;
            Destroy(gameObject);
        }
    }
}
