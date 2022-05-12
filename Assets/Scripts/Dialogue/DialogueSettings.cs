using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

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

[System.Serializable]
public class Sentence
{
    public string actorName;

    public Sprite actorImage;

    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;

    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class DialogEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();

        DialogueSettings settings = (DialogueSettings)target;

        Languages languages = new Languages();
        languages.portuguese = settings.sentence;

        Sentence sentence = new Sentence();
        sentence.actorImage = settings.profileImage;
        sentence.sentence = languages;

        if (GUILayout.Button("Create Dialogue"))
        {
            if (settings.sentence != null)
            {
                settings.sentences.Add(sentence);

                settings.profileImage = null;
                settings.sentence = "";
            }
        }

    }
}

#endif
