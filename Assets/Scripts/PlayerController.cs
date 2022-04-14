using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private float originalSpeed;

    private Rigidbody2D rig;
    private Vector2 direction;
    public bool IsWalking
    {
        get; private set;
    }
    private bool isRunning;

    public Vector2 Direction
    {
        get { return direction; }
    }


    public bool IsRunning
    {
        get { return isRunning; }
    }

    void Start()
    {
        originalSpeed = speed;
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //OnRun();
    }
    void FixedUpdate()
    {
        OnMove();
    }


    #region Movement

    void OnMove()
    {
        rig.MovePosition(
            rig.position
             + (direction * speed * Time.fixedDeltaTime)
        );
    }

    void OnRun(bool running)
    {
        if (running)
        {
            speed = runSpeed;
            isRunning = true;
        }
        else
        {
            speed = originalSpeed;
            isRunning = false;
        }
    }

    #endregion

    #region New Input System

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.canceled)
        {
            direction = Vector2.zero;
            IsWalking = false;
        }
        else
        {
            IsWalking = true;
            direction = value.ReadValue<Vector2>();
        }
    }

    public void OnRun(InputAction.CallbackContext value)
    {
        if (value.started && IsWalking)
            OnRun(true);
        if (value.canceled)
            OnRun(false);
    }

    #endregion
}
