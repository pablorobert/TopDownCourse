using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private PlayerController player;
    private bool isLookingLeft;
    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        FacePlayer();
    }

    void FacePlayer()
    {
        Vector2 direction = player.transform.position - this.transform.position;
        if ((direction.x >= 0 && isLookingLeft) || (direction.x < 0 && !isLookingLeft))
        {
            isLookingLeft = !isLookingLeft; //invert
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale =
            new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );
    }


}
