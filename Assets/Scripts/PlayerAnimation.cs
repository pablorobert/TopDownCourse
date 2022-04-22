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
    public const int PLAYER_WATERING = 5;
    public const int PLAYER_ATTACKING = 6;
    public const int BASE_LAYER = 0;
    public const string ANIMATOR_PROPERTY = "transition";
    public const string ANIMATOR_ROLLING_TRIGGER = "rolling";
    public const string ANIMATOR_FISHING_TRIGGER = "fishing";
    public const string ANIMATOR_HIT_TRIGGER = "hurt";
    public const string ANIMATOR_ROLL_NAME = "Rolling";
    public const string ANIMATOR_HAMMERING_PROPERTY = "hammering";

    private PlayerController playerController;
    private PlayerItems playerItems;
    private Animator anim;

    private Fishing fishing;

    private bool isHit;
    private float timeCount;
    public float recoveryTime = 1f;

    void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        fishing = FindObjectOfType<Fishing>();
    }
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
        OnWater();
        OnAttack();

        RecoveryTime();
    }

    void OnMove()
    {
        if (playerController.Direction.sqrMagnitude > 0)
        {
            if (playerController.IsRolling)
            {
                if (!anim.GetCurrentAnimatorStateInfo(BASE_LAYER).IsName(ANIMATOR_ROLL_NAME))
                {
                    playerController.IsRolling = false;
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

    #endregion

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

    void OnWater()
    {
        //print(playerController.IsWatering);
        if (playerController.IsWatering && playerItems.currentWater > 0)
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_WATERING);
        }
    }

    public void OnCast()
    {
        playerController.IsPaused = true;
        anim.SetTrigger(ANIMATOR_FISHING_TRIGGER);
    }

    public void OnCatch()
    {
        playerController.IsPaused = false;
        fishing.OnCatch();
    }

    public void OnHammer(bool started)
    {
        playerController.IsPaused = started;
        anim.SetBool(ANIMATOR_HAMMERING_PROPERTY, started);
    }

    public void OnAttack()
    {
        if (playerController.IsAttacking)
        {
            anim.SetInteger(ANIMATOR_PROPERTY, PLAYER_ATTACKING);
        }

    }

    public void OnHit()
    {
        if (!isHit)
        {
            isHit = true;
            anim.SetTrigger(ANIMATOR_HIT_TRIGGER);
        }
    }

    void RecoveryTime()
    {
        if (isHit)
        {

            timeCount += Time.deltaTime;
            if (timeCount >= recoveryTime)
            {
                timeCount = 0;
                isHit = false;
            }
        }
    }

}
