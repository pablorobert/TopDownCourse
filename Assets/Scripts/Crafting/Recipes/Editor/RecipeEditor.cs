using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{
    public const int SIZE = 64;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Recipe recipeScriptableObject = (Recipe)target;

        GUIStyle textStyle = new GUIStyle { fontStyle = FontStyle.Bold };
        textStyle.normal.textColor = Color.white;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("outcome", textStyle);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginVertical();

        //Texture texture = null;
        Sprite sprite = null;
        if (recipeScriptableObject.outcome != null)
        {
            sprite = recipeScriptableObject.outcome.sprite;
        }

        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("outcome"), GUIContent.none, true, GUILayout.Width(SIZE));

        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();


        GUILayout.Label("Recipe", textStyle);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical();
            sprite = null;

            if (recipeScriptableObject.item_02 != null)
            {
                sprite = recipeScriptableObject.item_02.sprite;
            }

            EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("item_02"), GUIContent.none, true, GUILayout.Width(SIZE));
            EditorGUILayout.EndVertical();
        }

        {
            EditorGUILayout.BeginVertical();

            sprite = null;
            if (recipeScriptableObject.item_12 != null)
            {
                sprite = recipeScriptableObject.item_12.sprite;
            }
            EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("item_12"), GUIContent.none, true, GUILayout.Width(SIZE));

            EditorGUILayout.EndVertical();
        }

        {
            EditorGUILayout.BeginVertical();

            sprite = null; ;
            if (recipeScriptableObject.item_22 != null)
            {
                sprite = recipeScriptableObject.item_22.sprite;
            }
            EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("item_22"), GUIContent.none, true, GUILayout.Width(SIZE));
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        sprite = null;

        if (recipeScriptableObject.item_01 != null)
        {

            sprite = recipeScriptableObject.item_01.sprite;
        }
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("item_01"), GUIContent.none, true, GUILayout.Width(SIZE));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        sprite = null;

        if (recipeScriptableObject.item_11 != null)
        {
            sprite = recipeScriptableObject.item_11.sprite;
        }
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("item_11"), GUIContent.none, true, GUILayout.Width(SIZE));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        sprite = null;
        if (recipeScriptableObject.item_21 != null)
        {
            sprite = recipeScriptableObject.item_21.sprite;
        }
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("item_21"), GUIContent.none, true, GUILayout.Width(SIZE));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();




        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        sprite = null;
        if (recipeScriptableObject.item_00 != null)
        {
            sprite = recipeScriptableObject.item_00.sprite;
        }
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("item_00"), GUIContent.none, true, GUILayout.Width(SIZE));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();

        sprite = null;
        if (recipeScriptableObject.item_10 != null)
        {
            sprite = recipeScriptableObject.item_10.sprite;
        }
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("item_10"), GUIContent.none, true, GUILayout.Width(SIZE));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        sprite = null;
        if (recipeScriptableObject.item_20 != null)
        {
            sprite = recipeScriptableObject.item_20.sprite;
        }
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("item_20"), GUIContent.none, true, GUILayout.Width(SIZE));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    /*private void DrawOnGUISprite(Sprite aSprite)
    {
        Rect c = aSprite.rect;
        float spriteW = c.width;
        float spriteH = c.height;
        Rect rect = GUILayoutUtility.GetRect(spriteW + 30, spriteH + 30);
        if (Event.current.type == EventType.Repaint)
        {
            var tex = aSprite.texture;
            c.xMin /= tex.width;
            c.xMax /= tex.width;
            c.yMin /= tex.height;
            c.yMax /= tex.height;

            GUI.DrawTextureWithTexCoords(rect, tex, c);
        }
    }

    private Texture2D GenerateTextureFromSprite(Sprite aSprite)
    {
        var rect = aSprite.rect;
        var tex = new Texture2D((int)rect.width, (int)rect.height);
        var data = aSprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        tex.SetPixels(data);
        tex.Apply(true);
        return tex;
    }

    private void crateBox()
    {
        EditorGUILayout.BeginVertical();

        //Texture texture = null;
        Sprite sprite = null;
        //if (recipeScriptableObject.outcome != null)
        {
            //texture = recipeScriptableObject.outcome.sprite.texture;
            //sprite = recipeScriptableObject.outcome.sprite;
        }
        //GUI.enabled = false;
        EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64));
        //GUI.enabled = true;
        //Texture2D tex = recipeScriptableObject.outcome.sprite.texture;
        //GUILayout.Box(tex, GUILayout.Width(SIZE), GUILayout.Height(SIZE));
        //GUILayout.Box(texture, GUILayout.Width(150), GUILayout.Height(150));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("outcome"), GUIContent.none, true, GUILayout.Width(SIZE));
        //DrawOnGUISprite(recipeScriptableObject.outcome.sprite);
        EditorGUILayout.EndVertical();
    }*/

}
