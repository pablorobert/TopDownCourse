using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip itemSound;
    private bool isDig;
    public int digAmount;

    public float waterAmount;
    private float currentWater;
    private int originalDigAmount;

    public Sprite hole;

    public Sprite carrot;

    private SpriteRenderer spriteRenderer;
    private PlayerItems playerItems;
    private PlayerController playerController;

    private bool isDetectingPlayer;
    private bool playerWatering;
    private bool isHoleVisible;
    private bool isCarrotVisible;

    void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        originalDigAmount = digAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoleVisible && playerWatering)
        {
            currentWater += 0.01f;
        }
        if (currentWater >= waterAmount && isHoleVisible)
        {
            audioSource.PlayOneShot(itemSound);
            spriteRenderer.sprite = carrot;
            isCarrotVisible = true;
            isHoleVisible = false;
        }
        if (isCarrotVisible && isDetectingPlayer && playerController.IsActing &&
        !playerItems.IsCarrotFull())
        {
            //harvest
            audioSource.PlayOneShot(itemSound);
            spriteRenderer.sprite = hole;
            playerItems.AddCarrot(1);
            isCarrotVisible = false;
            isHoleVisible = true;
            currentWater = 0;
            waterAmount *= 1.1f; //10% increase
        }

    }
    void OnHit()
    {
        digAmount--;
        if (digAmount < originalDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            isHoleVisible = true;
        }
        else if (digAmount <= 0)
        {
            digAmount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isDig && collider.CompareTag("Shovel"))
        {
            OnHit();
        }
        if (collider.CompareTag("Water"))
        {
            playerWatering = true;
        }
        if (collider.CompareTag("Player"))
        {
            isDetectingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isDetectingPlayer = false;
        }
        if (collider.CompareTag("Water"))
        {
            playerWatering = false;
        }
    }
}
