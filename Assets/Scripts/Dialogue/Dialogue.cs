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

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        GetSentences();
    }

    void Update()
    {
        if (isPlayerDetected && player.IsSpeaking)
        {
            DialogueController.Instance.Speak(sentences.ToArray());
        }
    }

    void FixedUpdate()
    {
        ShowDialog();
    }

    void GetSentences()
    {
        sentences.Clear();
        for (int i = 0; i < dialogue.sentences.Count; i++)
        {
            sentences.Add(dialogue.sentences[i].sentence.portuguese);
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
