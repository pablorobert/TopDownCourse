using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Languages))]
public class LanguagesPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        //EditorGUI.LabelField(position, label, new GUIContent("TODO"));

        EditorGUI.BeginProperty(position, label, property);
        Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);

        property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);
        int lines = 1;
        var indent = EditorGUI.indentLevel;

        if (property.isExpanded)
        {

            /*Rect rectType = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rectType, property.FindPropertyRelative("portuguese"));

            var myIcon = Resources.Load<Texture2D>("icons/brazil");
            GUI.DrawTexture(new Rect(position), myIcon);

            Rect rectDuration = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rectDuration, property.FindPropertyRelative("english"));

            Rect rectCooldown = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rectCooldown, property.FindPropertyRelative("spanish"));*/

            /*var originalRect = new Rect(position);
            var originalWidth = position.width;
            var originalX = position.x;
            var originalHeight = position.height;*/

            //var originalRect = new Rect(position);

            //position.width *= 0.75f;
            EditorGUI.indentLevel = 1;
            Rect rectBR = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            rectBR.width *= 0.90f;
            EditorGUI.PropertyField(rectBR, property.FindPropertyRelative("portuguese"));

            rectBR.x += rectBR.width + 5;
            //rectBR.width /= 5f;
            rectBR.width = 25;
            rectBR.height = 20;
            var brIcon = Resources.Load<Texture2D>("icons/brazil");
            GUI.DrawTexture(rectBR, brIcon);

            EditorGUI.indentLevel = 1;
            Rect rectUSA = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            rectUSA.width *= 0.90f;
            EditorGUI.PropertyField(rectUSA, property.FindPropertyRelative("english"));

            rectUSA.x += rectUSA.width + 5;
            //rectUSA.width /= 4f;
            rectUSA.width = 25;
            rectUSA.height = 20;
            //rectUSA.y += 2;
            var usaIcon = Resources.Load<Texture2D>("icons/usa");
            GUI.DrawTexture(rectUSA, usaIcon);

            EditorGUI.indentLevel = 1;
            Rect rectSpain = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            rectSpain.width *= 0.90f;
            EditorGUI.PropertyField(rectSpain, property.FindPropertyRelative("spanish"));

            rectSpain.x += rectSpain.width + 5;
            //rectSpain.width /= 4f;
            rectSpain.width = 25;
            rectSpain.height = 20;
            //rectSpain.y += 3;
            var spainIcon = Resources.Load<Texture2D>("icons/spain");
            GUI.DrawTexture(rectSpain, spainIcon);


        }

        /*Rect rectHelpBox = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
        EditorGUI.HelpBox(rectHelpBox, "This is our property drawer", MessageType.Info);*/

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();


        /*EditorGUI.BeginProperty(position, label, property);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        SerializedProperty portuguese = property.FindPropertyRelative("portuguese");
        SerializedProperty english = property.FindPropertyRelative("english");
        SerializedProperty spanish = property.FindPropertyRelative("spanish");

        Rect col = position;
        Rect ptRect = col;
        //col.width = position.width * 0.7f;
        //Rect soundRect = col;
        EditorGUI.PropertyField(ptRect, portuguese, GUIContent.none);

        Rect enRect = col;
        enRect.y += col.height;
        EditorGUI.PropertyField(enRect, english, GUIContent.none);

        Rect spRect = enRect;
        spRect.y += col.height;
        EditorGUI.PropertyField(spRect, spanish, GUIContent.none);

        EditorGUI.indentLevel = indent;
 
        EditorGUI.EndProperty();*/

        /*label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        contentPosition.width *= 0.75f;
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("portuguese"), GUIContent.none);
        contentPosition.x += contentPosition.width;
        contentPosition.width /= 3f;
        EditorGUIUtility.labelWidth = 14f;
        //EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("color"), new GUIContent("C"));
        EditorGUI.LabelField(contentPosition, new GUIContent(Brasilflag));
        EditorGUI.EndProperty();*/



        /*base.OnGUI(position, property, label);*/

        /*EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent1 = EditorGUI.indentLevel; //save
        EditorGUI.indentLevel = 0;

        var portugueseRect = new Rect(position.x, position.y, 30, position.height);

        EditorGUI.PropertyField(portugueseRect, property.FindPropertyRelative("portuguese"), GUIContent.none);

        EditorGUI.indentLevel = indent1;
        //EditorGUI.EndProperty();
        //EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent2 = EditorGUI.indentLevel; //save
        EditorGUI.indentLevel = 0;

        var englishRect = new Rect(position.x, position.y + position.height, 30, position.height);
        
        EditorGUI.PropertyField(englishRect, property.FindPropertyRelative("english"), GUIContent.none);

        EditorGUI.indentLevel = indent2;
        //EditorGUI.EndProperty();
        //EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent3 = EditorGUI.indentLevel; //save
        EditorGUI.indentLevel = 0;

        var spanishRect = new Rect(position.x, position.y + (2 * position.height), 30, position.height);
        
        EditorGUI.PropertyField(spanishRect, property.FindPropertyRelative("spanish"), GUIContent.none);

        EditorGUI.indentLevel = indent3;
        EditorGUI.EndProperty();*/

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 2;

        if (property.isExpanded)
        {
            totalLines += 4;
        }

        return EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
    }
}
