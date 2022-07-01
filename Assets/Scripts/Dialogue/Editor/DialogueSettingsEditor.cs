using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueSettings))]
public class DialogSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

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
