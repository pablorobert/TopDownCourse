using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite profileImage;

    [TextArea]
    public string sentence;

    public List<Sentence> sentences = new List<Sentence>();

    public UnityAction OnComplete;
    public void RaiseEvent()
    {
        OnComplete?.Invoke();
    }
}