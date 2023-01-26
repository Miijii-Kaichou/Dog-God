using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowAsSystemIndicator))]
public class SystemInfoDrawer : PropertyDrawer
{
    GUIStyle currentStyle;
    Color stopped = new Color(1f, 0f, 0f, 0.5f);
    Color paused = new Color(0.5f, 0.5f, 0f, 0.5f);
    Color running = new Color(0f, 1f, 0f, 0.5f);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var colorIndicator = new Rect(position.x, position.y, position.width - 220, position.height);
        var systemNameRect = new Rect(position.x + position.width - 215f, position.y, 80f, position.height);
        var systemObjRect = new Rect(position.x + position.width - 150f, position.y, 150f, position.height);

        switch (property.FindPropertyRelative("systemStatus.systemStatus").enumValueIndex)
        {
            case 0:
                {
                    currentStyle = new GUIStyle(GUI.skin.box);
                    currentStyle.normal.background = MakeTex(2, 2, stopped);
                }
                break;
            case 1:
                {
                    currentStyle = new GUIStyle(GUI.skin.box);
                    currentStyle.normal.background = MakeTex(2, 2, paused);
                }
                break;
            case 2:
                {
                    currentStyle = new GUIStyle(GUI.skin.box);
                    currentStyle.normal.background = MakeTex(2, 2, running);
                }
                break;
        }

        GUI.Box(colorIndicator, "", currentStyle);
        EditorGUI.PropertyField(systemNameRect, property.FindPropertyRelative("systemName"), GUIContent.none);
        EditorGUI.PropertyField(systemObjRect, property.FindPropertyRelative("system"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
