using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    PlayerController playerController;
    NavMeshAgent agent;

    Animator anim;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
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
}
