using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public enum PlayerTools
{
    Axe,
    Shovel,
    Bucket,
    Sword
}

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
    }

    [SerializeField]
    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private bool criticalDamage;

    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private PlayerTools activeTool = PlayerTools.Axe;

    public PlayerTools ActiveTool
    {
        get { return activeTool; }
    }

    private float originalSpeed;
    private float waterTime;
    private bool decreaseWater;

    [Header("Attack")]

    public Transform swordPosition;
    public float attackRange;

    public LayerMask enemyLayer;

    [Header("Events")]
    public UnityEvent OnChangeTool;

    private Rigidbody2D rig;
    private PlayerItems playerItems;
    private Vector2 direction;

    private SpriteRenderer spriteRenderer;

    public bool IsPaused
    {
        get; set;
    }
    public bool IsWalking
    {
        get; private set;
    }

    public bool IsRunning
    {
        get; set;
    }

    public bool IsRolling
    {
        get; set;
    }
    public bool IsCutting
    {
        get; private set;
    }
    public bool IsDigging
    {
        get; private set;
    }
    public bool IsWatering
    {
        get; private set;
    }
    public bool IsAttacking
    {
        get; private set;
    }
    public bool IsActing
    {
        get; set;
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
        currentHealth = maxHealth;
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        OnChangeTool?.Invoke();
    }

    void Update()
    {
        /*Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame) {

        }
        Mouse mouse = InputSystem.GetDevice<Mouse>();
        if (mouse.rightButton.wasPressedThisFrame) {

        }*/

        CriticalDamage();
        if (IsPaused) return;

        CheckWater();
    }
    void FixedUpdate()
    {
        if (IsPaused) return;

        OnMove();
    }

    void CriticalDamage()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth < 3)
        {
            criticalDamage = true;
        }
        else
        {
            criticalDamage = false;
        }

        if (criticalDamage)
        {
            spriteRenderer.color = Color.Lerp(
                Color.white,
                Color.red,
                Mathf.PingPong(8 * Time.time, 0.5f)
            );
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void CheckWater()
    {
        if (!decreaseWater)
            return;

        if (IsWatering)
        {
            if (playerItems.currentWater > 0)
            {
                waterTime += Time.deltaTime;
                if (waterTime > 1f) //decrease for second
                {
                    waterTime = 0;
                    playerItems.currentWater--;
                }
            }
            else
            {
                IsWatering = false; //we dont have water
            }
        }
    }

    public void MoveToPosition(Vector2 pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }

    public void FaceGameObject(Transform t)
    {
        float relativePos = t.position.x - transform.position.x;

        if (relativePos > 0)
        {
            Flip(false); // false = right 
        }
        else
        {
            Flip(true); // true = left
        }
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

    void OnAct(bool acting)
    {
        IsActing = acting;
    }

    void OnTool(bool toolling)
    {
        speed = toolling ? 0f : originalSpeed;

        IsCutting = activeTool == PlayerTools.Axe ? toolling : false;
        IsDigging = activeTool == PlayerTools.Shovel ? toolling : false;
        IsWatering = activeTool == PlayerTools.Bucket ? toolling : false;
        IsAttacking = activeTool == PlayerTools.Sword ? toolling : false;
    }

    void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(swordPosition.position, attackRange, enemyLayer);
        if (hit != null)
        {
            /*if (hit.TryGetComponent<Skeleton>(out Skeleton enemy))
            {
                enemy.OnHit();
            }*/
            hit.GetComponent<Skeleton>().OnHit();
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
        if (value.started && direction.sqrMagnitude > 0)
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
        if (value.started)
        {
            OnTool(true);
        }
        if (value.performed)
        {
            decreaseWater = true;
        }
        if (value.canceled)
        {
            decreaseWater = false;
            OnTool(false);
        }
    }

    public void OnAct(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            OnAct(true);
        }
        if (value.canceled)
        {
            OnAct(false);
        }
    }

    public void OnAxe(InputAction.CallbackContext value)
    {
        if (IsDigging || IsWatering || IsAttacking) return;
        activeTool = PlayerTools.Axe;
        OnChangeTool?.Invoke();
    }

    public void OnShovel(InputAction.CallbackContext value)
    {
        if (IsCutting || IsWatering || IsAttacking) return;
        activeTool = PlayerTools.Shovel;
        OnChangeTool?.Invoke();
    }

    public void OnBucket(InputAction.CallbackContext value)
    {
        if (IsCutting || IsDigging || IsAttacking) return;
        activeTool = PlayerTools.Bucket;
        OnChangeTool?.Invoke();
    }

    public void OnSword(InputAction.CallbackContext value)
    {
        if (IsCutting || IsDigging || IsWatering) return;
        activeTool = PlayerTools.Sword;
        OnChangeTool?.Invoke();
    }

    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(swordPosition.position, attackRange);
    }

    public void Flip(bool left)
    {
        if (left)
        {
            transform.eulerAngles = new Vector2(0, 180f);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }
}
