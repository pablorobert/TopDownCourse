using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{
    public const int SIZE = 64;
    public const int ELEMENTS = 9;
    public const int COLUMNS = 3;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Recipe recipeScriptableObject = (Recipe)target;

        GUIStyle textStyle = new GUIStyle { fontStyle = FontStyle.Bold };
        textStyle.normal.textColor = Color.white;

        //SerializedObject sObj = new SerializedObject(target);

        using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("Outcome", textStyle);
            GUILayout.FlexibleSpace();
        }
        
        Sprite sprite = null;
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginVertical();
            
            if (recipeScriptableObject.outcome != null)
            {
                sprite = recipeScriptableObject.outcome.sprite;
            }

            EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("outcome"), GUIContent.none, true, GUILayout.Width(SIZE));

            EditorGUILayout.EndVertical();

            GUILayout.FlexibleSpace();
        }

        EditorGUILayout.Space();
        GUILayout.Label("Recipe", textStyle);

        // start of recipe items
        SerializedProperty propItems = serializedObject.FindProperty("items");

        int currentRow = 0;
        for (int i = 0; i < ELEMENTS; i++)
        {
            SerializedProperty propItem = propItems.GetArrayElementAtIndex(i); //item of array

            using (new GUILayout.HorizontalScope())
            {
                if (i % COLUMNS == 0)
                {
                    currentRow++;
                    EditorGUILayout.BeginHorizontal();
                }
                
                //stack the two elements vertically
                EditorGUILayout.BeginVertical();
                sprite = null;
                Item itObj = null;

                if (propItem != null)
                {
                    itObj = propItem.objectReferenceValue as Item;
                }

                EditorGUILayout.ObjectField(itObj?.sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
                EditorGUILayout.PropertyField(propItem, GUIContent.none, true, GUILayout.Width(SIZE));

                EditorGUILayout.EndVertical();
                
                if (i == ((COLUMNS * currentRow - 1)))
                {
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        //end of recipe items

        serializedObject.ApplyModifiedProperties();
    }
}
