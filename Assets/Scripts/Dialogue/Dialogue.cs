using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    public float dialogueRange;

    public LayerMask playerMask;

    private bool isPlayerDetected;

    public bool IsPlayerDetected { get => isPlayerDetected; }
    //public bool IsPlayerDetected => isPlayerDetected;

    public DialogueSettings dialogue;

    private PlayerController player;

    private NPC npc;

    [Header("Quest")]
    public bool hasQuest;
    private NPCQuest npcQuest;
    private DialogueSettings settings;

    public DialogueSettings dialogueBetweenQuest;
    public DialogueSettings dialogueAfterQuest;

    void Awake()
    {
        npc = GetComponent<NPC>();
        if (hasQuest)
        {
            npcQuest = GetComponent<NPCQuest>();
        }
    }

    void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    void Update()
    {
        if (hasQuest && npcQuest.isActive &&
            GameManager.Instance.QuestManager.quests[npcQuest.questIndex].isCompleted)
        {

            if (dialogueAfterQuest != null)
            {
                dialogue = dialogueAfterQuest;
            }
        }

        if (isPlayerDetected && player.IsSpeaking)
        {
            DialogueController.Instance.SetNpc(npc);
            DialogueController.Instance.Speak(dialogue);
        }
    }

    void FixedUpdate()
    {
        ShowDialog();
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
                //GetDialogueInfo();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}