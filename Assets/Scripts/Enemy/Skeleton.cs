using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;

    public Image lifeBar;

    [SerializeField] private int currentHealth;
    public int maxHealth = 10;
    private bool isDead;
    private PlayerController playerController;
    private PlayerAnimation playerAnim;
    private NavMeshAgent agent;
    private Animator anim;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerAnim = playerController.GetComponent<PlayerAnimation>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
            return;

        agent.SetDestination(playerController.transform.position);

        if (Vector2.Distance(transform.position, playerController.transform.position) <=
            agent.stoppingDistance)
        {
            //idle
            PlayAnimation(2);
        }
        else
        {
            //walking
            PlayAnimation(1);
        }

        if (playerController.transform.position.x - transform.position.x > 0)
        {
            //right
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else
        {
            //left
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void PlayAnimation(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (isDead)
            return;

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hit != null)
        {
            playerAnim.OnHit();
        }
    }

    public void OnHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetTrigger("death");
            isDead = true;
            Destroy(gameObject, 2f);
        }
        else
        {
            anim.SetTrigger("hurt");
        }
        lifeBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
