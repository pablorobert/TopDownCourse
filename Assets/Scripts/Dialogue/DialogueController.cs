using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public enum Locales
{
    [InspectorName("Português")] pt_BR,
    [InspectorName("English")] en_US,
    [InspectorName("Spañol")] es_ES
}

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;

    [Header("Components")]
    public GameObject dialogueWindow;
    public Image profileImage;
    public TMP_Text speechText;
    public Text actorNameText;
    public Button nextSentenceButton;
    public TMP_Text nextSentenceText;

    [HideInInspector] public DialogueSettings dialogueSettings;

    [Header("Settings")]
    public float typingSpeed;

    public Locales locale;

    //control 
    public bool IsVisible
    {
        get; private set;
    }

    private bool isButtonVisible;

    private int currentIndex;

    private WaitForSeconds waitForSeconds;

    private PlayerController playerController;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerController = GameManager.Instance.GetPlayer();
        waitForSeconds = new WaitForSeconds(typingSpeed);
    }

    IEnumerator TypeSentence()
    {
        speechText.text = "";
        nextSentenceButton.gameObject.SetActive(false);
        nextSentenceText.text = ">>";
        if (currentIndex == dialogueSettings.sentences.Count - 1)
        {
            nextSentenceText.text = "End";
        }

        string sentence = GetSentence(currentIndex);
        foreach (char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return waitForSeconds;
        }
        nextSentenceButton.gameObject.SetActive(true);
    }

    public void NextSentence()
    {
        string actorName;
        Sprite actorImage;
        int lastIndex = currentIndex;
        currentIndex++;
        if (currentIndex == dialogueSettings.sentences.Count) //out of sentences
        {
            dialogueWindow.SetActive(false);
            IsVisible = false;
            playerController.IsPaused = false;
            playerController.GetComponentInChildren<PlayerInput>().actions.Enable();

            //conversation is done, warn listeners
            dialogueSettings.RaiseEvent();

            return;
        }
        if (lastIndex < dialogueSettings.sentences.Count - 1 &&
        GetSentence(lastIndex) == speechText.text)
        {
            speechText.text = "";
            actorName = dialogueSettings.sentences[currentIndex].actorName;
            actorImage = dialogueSettings.sentences[currentIndex].actorImage;

            if (actorName != null && actorName != "")
            {
                actorNameText.text = actorName;
            }
            if (actorImage != null)
            {
                profileImage.sprite = actorImage;
            }
            StartCoroutine(TypeSentence());
        }
    }

    public string GetSentence(int index)
    {
        switch (DialogueController.Instance.locale)
        {
            case Locales.pt_BR:
                return dialogueSettings.sentences[index].sentence.portuguese;
            case Locales.en_US:
                return dialogueSettings.sentences[index].sentence.english;
            case Locales.es_ES:
                return dialogueSettings.sentences[index].sentence.spanish;
        }
        return ""; //should not be here 
    }

    public void Speak(DialogueSettings settings)
    {
        if (!IsVisible)
        {
            IsVisible = true;
            this.dialogueSettings = settings;

            playerController.IsPaused = true;
            playerController.GetComponentInChildren<PlayerInput>().actions.Disable();

            currentIndex = 0;

            speechText.text = "";
            string actorName = dialogueSettings.sentences[currentIndex].actorName;
            Sprite actorImage = dialogueSettings.sentences[currentIndex].actorImage;

            if (actorName != null)
            {
                actorNameText.text = actorName;
            }
            else
            {
                actorNameText.text = "";
            }
            if (actorImage != null)
            {
                profileImage.gameObject.SetActive(true);
                profileImage.sprite = actorImage;
            }
            else
            {
                profileImage.gameObject.SetActive(false);
            }

            dialogueWindow.SetActive(true);

            StartCoroutine(TypeSentence());
        }
    }
}
