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

    public bool IsRunning
    {
        get; private set;
    }

    public bool IsRolling
    {
        get; private set;
    }

    public bool IsCutting
    {
        get; private set;
    }

    public bool IsSpeaking
    {
        get; set;
    }

    public Vector2 Direction
    {
        get { return direction; }
    }

    void Start()
    {
        originalSpeed = speed;
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //OnRun();

        /*Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame) {

        }

        Mouse mouse = InputSystem.GetDevice<Mouse>();
        if (mouse.rightButton.wasPressedThisFrame) {

        }*/
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
            IsRunning = true;
        }
        else
        {
            speed = originalSpeed;
            IsRunning = false;
        }
    }

    void OnRoll(bool roll)
    {
        if (roll)
        {
            speed = runSpeed;
        }
        else
        {
            speed = originalSpeed;
        }
        IsRolling = roll;
    }

    void OnTalk(bool talking)
    {
        IsSpeaking = talking;
    }

    void OnTool(bool toolling)
    {
        if (toolling)
        {
            speed = 0f;
        }
        else
        {
            speed = originalSpeed;
        }
        IsCutting = toolling;
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
        else if (value.performed)
        {
            IsWalking = true;
            direction = value.ReadValue<Vector2>();
        }
    }

    public void OnRun(InputAction.CallbackContext value)
    {
        if (value.performed && IsWalking)
            OnRun(true);
        if (value.canceled)
            OnRun(false);
    }

    public void OnRoll(InputAction.CallbackContext value)
    {
        if (value.performed && direction.sqrMagnitude > 0)
            OnRoll(true);
        if (value.canceled)
            OnRoll(false);
    }

    public void OnTalk(InputAction.CallbackContext value)
    {
        if (value.performed)
            OnTalk(true);
        if (value.canceled)
            OnTalk(false);
    }

    public void OnTool(InputAction.CallbackContext value)
    {
        if (value.performed)
            OnTool(true);
        if (value.canceled)
            OnTool(false);
    }

    #endregion
}
