using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public float dialogueRange;

    public LayerMask playerMask;

    private bool isPlayerDetected;

    public DialogueSettings dialogue;

    private PlayerController player;

    private List<string> sentences = new List<string>();
    private List<string> actorNames = new List<string>();
    private List<Sprite> actorImages = new List<Sprite>();


    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        GetDialogueInfo();
    }

    void Update()
    {
        if (isPlayerDetected && player.IsSpeaking)
        {
            DialogueController.Instance.Speak(
                sentences.ToArray(),
                actorNames.ToArray(),
                actorImages.ToArray()
            );
        }
    }

    void FixedUpdate()
    {
        ShowDialog();
    }

    void GetDialogueInfo()
    {

        sentences.Clear();
        actorNames.Clear();
        actorImages.Clear();

        for (int i = 0; i < dialogue.sentences.Count; i++)
        {
            switch (DialogueController.Instance.locale)
            {
                case Locales.pt_BR:
                    sentences.Add(dialogue.sentences[i].sentence.portuguese);
                    break;
                case Locales.en_US:
                    sentences.Add(dialogue.sentences[i].sentence.english);
                    break;
                case Locales.es_ES:
                    sentences.Add(dialogue.sentences[i].sentence.spanish);
                    break;
            }

            actorNames.Add(dialogue.sentences[i].actorName);
            actorImages.Add(dialogue.sentences[i].actorImage);
        }
    }

    void Speak()
    {
        if (!isPlayerDetected)
            return;
    }

    void ShowDialog()
    {
        isPlayerDetected = false;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerMask);
        if (hit != null)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
