using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

[CustomEditor(typeof(ConstantsManager))]
public class ConstantsManagerInspector : Editor
{
    SerializedProperty constants;
    private Vector2 scrollPosition;
    private bool isInView = true;
    private bool[] elementSelected;
    private bool isGeneratingCacheFile;

    public void OnEnable()
    {
        constants = serializedObject.FindProperty("test");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Constant"))
        {
            constants.InsertArrayElementAtIndex(constants.arraySize - 1);
        }
        if (GUILayout.Button("Generate Constants Cache"))
        {
            ProduceGlobalConstantsFile();
        }
        GUILayout.EndHorizontal();
        isInView = EditorGUILayout.BeginFoldoutHeaderGroup(isInView, "Constants");
        GUILayout.BeginScrollView(scrollPosition);
        DisplayConstants();
        GUILayout.EndScrollView();
        EditorGUILayout.EndFoldoutHeaderGroup();
        serializedObject.ApplyModifiedProperties();
    }

    private void ProduceGlobalConstantsFile()
    {
        isGeneratingCacheFile = true;

        EditorUtility.DisplayProgressBar("Generating Constant Cache File...", "Structring File...", 0);
        // TODO: Create a .cs class at the root of Assets using the 
        // Constants template.
        StringBuilder newFile = new StringBuilder();
        newFile.Append("public static class Constants\n");
        newFile.Append("{\n\t");

        SerializedProperty constantName;
        SerializedProperty constantValue;
        SerializedProperty constantType;

        for (int i = 0; i < constants.arraySize; i++)
        {

            var currentElement = constants.GetArrayElementAtIndex(i);

            constantName = currentElement.FindPropertyRelative("Key");
            constantValue = currentElement.FindPropertyRelative("Value");
            constantType = currentElement.FindPropertyRelative("Type");

            EditorUtility.DisplayProgressBar("Generating Constant Cache File...", $"Generating Constant {constantName.stringValue}", (i / (constants.arraySize) - 1));

            var variableType = ((ConstantType)constantType.enumValueIndex).ToString().ToLower();
            var variableName = constantName.stringValue;
            var variableValue = ResolveType(constantType.enumValueIndex, constantValue.stringValue, out bool result);

            if (result == false) ResolveType(constantType.enumValueIndex, "0", out result);
            if (result == false)
            {
                Debug.Log("Failed to generate constants cache.");
                return;
            }
            newFile.Append($"public const {variableType} {variableName} = {variableValue};\n\t");
        }

        newFile.Append("}");

        // TODO: Create new .cs, and write to Asset folder
        using (StreamWriter sw = new(Application.dataPath + @"/Constants.cs"))
        {
            EditorUtility.DisplayProgressBar("Finalizing Constant Cache File Generation...", "Writing...", 0.9f);
            sw.Write(newFile.ToString());
        }

        EditorUtility.ClearProgressBar();

        isGeneratingCacheFile = false;

    }

    private object ResolveType(int enumValueIndex, string stringValue, out bool success)
    {
        success = true;
        try
        {
            switch ((ConstantType)enumValueIndex)
            {
                case ConstantType.INT: return Convert.ToInt32(stringValue);
                case ConstantType.FLOAT: return Convert.ToSingle(stringValue) + "f";
                case ConstantType.DOUBLE: return Convert.ToDouble(stringValue) + "d";
                case ConstantType.STRING: return $"\"{stringValue}\"";
                default: break;
            }
        }
        catch (Exception)
        {
            success = false;
        }
        return $"\"{stringValue}\"";
    }

    private void DisplayConstants()
    {
        elementSelected = new bool[constants.arraySize];
        EditorGUILayout.BeginFadeGroup(isInView ? 1 : 0);
        for (int i = 0; i < constants.arraySize; i++)
        {
            var currentElement = constants.GetArrayElementAtIndex(i);
            SerializedProperty constantName = currentElement.FindPropertyRelative("Key");
            SerializedProperty constantValue = currentElement.FindPropertyRelative("Value");
            SerializedProperty constantType = currentElement.FindPropertyRelative("Type");
            GUILayout.BeginHorizontal();
            GUILayout.Toggle(elementSelected[i], GUIContent.none);
            GUILayout.BeginVertical();
            GUILayout.Label("ID");
            constantName.stringValue = GUILayout.TextField(constantName.stringValue);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("Value");
            constantValue.stringValue = GUILayout.TextField(constantValue.stringValue);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("Type");
            constantType.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup((ConstantType)constantType.enumValueIndex));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFadeGroup();
    }
}
