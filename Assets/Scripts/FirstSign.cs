using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSign : MonoBehaviour
{
    public GameObject NPC1; //change her dialogue
    public DialogueSettings newDialogue;
    public float timeToDestroy = 0.5f;

    public DialogueSettings settings;

    public void Kill()
    {
        //Get the npc
        Dialogue dialogue = NPC1.GetComponent<Dialogue>();
        dialogue.dialogue = newDialogue;
        dialogue.GetDialogueInfo();

        //destroy this one
        gameObject.SetActive(false);
        Destroy(gameObject, timeToDestroy);
    }

    public void OnEnable()
    {
        settings.OnComplete += Kill;
    }

    public void OnDisable()
    {
        settings.OnComplete -= Kill;
    }

}
