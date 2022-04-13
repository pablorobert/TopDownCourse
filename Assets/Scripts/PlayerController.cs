using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rig;
    private Vector2 direction;

    public Vector2 Direction
    {
        get { return direction; }
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        OnRun();
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

    void OnRun()
    {

    }

    #endregion

    #region New Input System

    public void OnMovement(InputAction.CallbackContext value)
    {
        /*if (value.canceled)
        {
            direction = Vector2.zero;
        }
        else
        {
            direction = value.ReadValue<Vector2>();
        }*/
        direction = value.ReadValue<Vector2>();
    }

    #endregion
}
