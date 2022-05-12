using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToNewDialogue : MonoBehaviour
{
    public GameObject NPC; //change her dialogue
    public DialogueSettings newDialogue;

    public DialogueSettings settings;

    public void ChangeDialogue()
    {
        //Get the npc
        Dialogue dialogue = NPC.GetComponent<Dialogue>();
        dialogue.dialogue = newDialogue;
    }

    public void OnEnable()
    {
        settings.OnComplete += ChangeDialogue;
    }

    public void OnDisable()
    {
        settings.OnComplete -= ChangeDialogue;
    }

}