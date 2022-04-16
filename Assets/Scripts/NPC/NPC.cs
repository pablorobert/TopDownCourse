using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public const string ANIMATOR_NPC_ISWALKING = "isWalking";
    public bool randomWalk;
    public float speed;
    private float originalSpeed;
    public List<Transform> paths = new List<Transform>();
    private PlayerController player;
    private bool isLookingLeft;
    private int currentIndex;

    private Animator anim;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        originalSpeed = speed;
    }

    void Update()
    {
        //FacePlayer();
        ShouldWalk();
        Walk();
    }

    private void ShouldWalk()
    {
        if (DialogueController.Instance.IsVisible)
        {
            speed = 0;
            anim.SetBool(ANIMATOR_NPC_ISWALKING, false);
            FacePlayer();
        }
        else
        {
            anim.SetBool(ANIMATOR_NPC_ISWALKING, true);
            speed = originalSpeed;
        }
    }

    private void Walk()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            paths[currentIndex].position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, paths[currentIndex].position) < 0.1f)
        {

            if (randomWalk)
            {
                currentIndex = Random.Range(0, paths.Count);
            }
            else
            {
                if (currentIndex < paths.Count - 1)
                {
                    currentIndex++;
                }
                else
                {
                    currentIndex = 0;
                }
            }

        }

        Vector2 direction = paths[currentIndex].position - transform.position;

        if (direction.x >= 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180f);
        }
    }

    public void FacePlayer()
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
        if (isLookingLeft)
        {
            transform.eulerAngles = new Vector2(0, 180f);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        /*transform.localScale =
            new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );*/
    }
}
