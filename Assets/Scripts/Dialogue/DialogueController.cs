using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueWindow;

    public Image profileImage;

    //public Text speechText;
    public TMP_Text speechText;

    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    //control 
    private bool isVisible;

    private int currentIndex;

    private string[] sentences;

    public static DialogueController Instance;

    void Awake()
    {
        Instance = this;
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[currentIndex].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {

    }

    public void Speak(string[] sentences)
    {
        if (!isVisible)
        {
            isVisible = true;
            this.sentences = sentences;
            dialogueWindow.SetActive(true);
            StartCoroutine(TypeSentence());

        }
    }
}
