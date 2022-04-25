using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    public float dialogueRange;

    public LayerMask playerMask;

    private bool isPlayerDetected;

    public DialogueSettings dialogue;

    private PlayerController player;

    private List<string> sentences = new List<string>();
    private List<string> actorNames = new List<string>();
    private List<Sprite> actorImages = new List<Sprite>();

    private NPC npc;

    [Header("Quest")]
    public bool hasQuest;
    private NPCQuest npcQuest;
    private DialogueSettings settings;

    public DialogueSettings dialogueBetweenQuest;
    public DialogueSettings dialogueAfterQuest;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        npc = GetComponent<NPC>();
        if (hasQuest)
        {
            npcQuest = GetComponent<NPCQuest>();
        }
    }

    void Start()
    {
        GetDialogueInfo();
    }

    void Update()
    {
        if (hasQuest && npcQuest.isActive &&
            GameManager.Instance.QuestManager.quests[npcQuest.questIndex].isCompleted)
        {

            if (dialogueAfterQuest != null)
            {
                dialogue = dialogueAfterQuest;
                GetDialogueInfo();
            }
        }

        if (isPlayerDetected && player.IsSpeaking)
        {
            DialogueController.Instance.Speak(
                sentences.ToArray(),
                actorNames.ToArray(),
                actorImages.ToArray(),
                dialogue
            );
        }


    }

    void FixedUpdate()
    {
        ShowDialog();
    }

    public void GetDialogueInfo()
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
            if (npc != null) npc.FacePlayer();
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    public void OnEnable()
    {
        if (hasQuest)
        {
            dialogue.OnComplete += OnCompleteDialogue;
        }
    }

    public void OnDisable()
    {
        if (hasQuest)
        {
            dialogue.OnComplete -= OnCompleteDialogue;
        }
    }

    public void OnCompleteDialogue()
    {
        if (!npcQuest.isActive)
        {
            npcQuest.StartQuest();
            if (dialogueBetweenQuest != null)
            {
                dialogue = dialogueBetweenQuest;
                GetDialogueInfo();
            }
        }
        print("complete");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
