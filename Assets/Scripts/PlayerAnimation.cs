using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //should we use enum? 
    public const int PLAYER_IDLE = 0;
    public const int PLAYER_WALKING = 1;
    public const int PLAYER_RUNNING = 2;
    public const int PLAYER_CUTTING = 3;
    public const int PLAYER_DIGGING = 4;

    public const int BASE_LAYER = 0;
    public const string ANIMATOR_PROPERTY = "transition";
    public const string ANIMATOR_ROLLING_TRIGGER = "rolling";
    public const string ANIMATOR_ROLL_NAME = "roll";

    private PlayerController playerController;
    private Animator anim;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    #region Movement
    void Update()
    {
        OnMove();
        OnRun();
        OnCut();
        OnDig();
    }

    void OnMove()
    {
        if (playerController.Direction.sqrMagnitude > 0)
        {
            if (playerController.IsRolling)
            {
                if (!anim.GetCurrentAnimatorStateInfo(BASE_LAYER).IsName(ANIMATOR_ROLL_NAME))
                {
                    anim.SetTrigger(ANIMATOR_ROLLING_TRIGGER);
                }
            }
            else
            {
                anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_WALKING);
            }
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

    void OnRun()
    {
        if (playerController.IsRunning)
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_RUNNING);
        }
        else
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_IDLE);
        }
    }

    void OnCut()
    {
        if (playerController.IsCutting)
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_CUTTING);
        }
    }

    void OnDig()
    {
        if (playerController.IsDigging)
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_DIGGING);
        }
    }

    #endregion
}
