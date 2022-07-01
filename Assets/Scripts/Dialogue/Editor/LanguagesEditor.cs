using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Languages))]
public class LanguagesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //Languages languages = (Languages)target;

    }

}
