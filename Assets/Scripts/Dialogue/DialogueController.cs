using System.Collections;
using System.Collections.Generic;
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

    private string[] sentences;

    private string[] actorNames;

    private Sprite[] actorImages;

    private PlayerController playerController;
    void Awake()
    {
        Instance = this;
        playerController = FindObjectOfType<PlayerController>();
    }

    IEnumerator TypeSentence()
    {
        speechText.text = "";
        nextSentenceButton.gameObject.SetActive(false);
        nextSentenceText.text = ">>";
        if (currentIndex == sentences.Length - 1)
        {
            nextSentenceText.text = "End";
        }

        foreach (char letter in sentences[currentIndex].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextSentenceButton.gameObject.SetActive(true);
    }

    public void NextSentence()
    {
        int lastIndex = currentIndex;
        currentIndex++;
        if (currentIndex == sentences.Length) //out of sentences
        {
            dialogueWindow.SetActive(false);
            IsVisible = false;
            playerController.IsPaused = false;
            playerController.GetComponentInChildren<PlayerInput>().actions.Enable();

            //informs to listeners the conversation is done
            dialogueSettings.RaiseEvent();

            return;
        }
        if (lastIndex < sentences.Length - 1 &&
        sentences[lastIndex] == speechText.text)
        {
            speechText.text = "";
            if (actorNames[currentIndex] != null && actorNames[currentIndex] != "")
            {
                actorNameText.text = actorNames[currentIndex];
            }
            if (actorImages[currentIndex] != null)
            {
                profileImage.sprite = actorImages[currentIndex];
            }
            StartCoroutine(TypeSentence());
        }

    }

    public void Speak(string[] sentences, string[] actorNames, Sprite[] actorImages, DialogueSettings settings)
    {
        if (!IsVisible)
        {
            IsVisible = true;
            playerController.IsPaused = true;
            playerController.GetComponentInChildren<PlayerInput>().actions.Disable();

            this.dialogueSettings = settings;

            this.sentences = sentences;
            this.actorNames = actorNames;
            this.actorImages = actorImages;

            currentIndex = 0;

            speechText.text = "";
            if (actorNames[currentIndex] != null)
            {
                actorNameText.text = actorNames[currentIndex];
            }
            else
            {
                actorNameText.text = "";
            }
            if (actorImages[currentIndex] != null)
            {
                profileImage.gameObject.SetActive(true);
                profileImage.sprite = actorImages[currentIndex];
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
