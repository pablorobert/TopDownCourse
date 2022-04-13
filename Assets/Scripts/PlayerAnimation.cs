using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public const int PLAYER_IDLE = 0;
    public const int PLAYER_WALKING = 1;

    public const string ANIMATOR_PROPERTY = "transition";

    private PlayerController playerController;
    private Animator anim;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerController.Direction.sqrMagnitude > 0)
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_WALKING);
        }
        else
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_IDLE);
        }

        if (playerController.Direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (playerController.Direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
}
