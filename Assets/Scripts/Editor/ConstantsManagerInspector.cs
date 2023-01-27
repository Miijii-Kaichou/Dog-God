using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Codice.CM.Common;

[CustomEditor(typeof(ConstantsManager))]
public class ConstantsManagerInspector : Editor
{
    ConstantsManager self;

    SerializedProperty constants;
    private Vector2 scrollPosition;
    private bool generateOnConstantListChangeAllowed;
    private bool promptOnConstantRemovalAllowed = true;
    private bool isInView = true;
    private bool changeLastEntryName;

    private bool CacheExists => File.Exists(Application.dataPath + ConstantCacheFile);

    private const int MaxScrollHeight = 250;
    private const float MaxWidth = 100;
    private const string ClassDefinition = "public static class Constants\n";
    private const string StringPropertyName = "Key";
    private const string StringPropertyValue = "Value";
    private const string StringPropertyType = "Type";
    private const string ConstantVariableFormat = "public const {0} {1} = {2};\n";
    private const string ConstantCacheFile = @"/Constants.cs";
    private const string LabelID = "ID";
    private const string LavelValue = "Value";
    private const string LabelType = "Type";

    public void OnEnable()
    {
        self = (ConstantsManager)target;
        constants = serializedObject.FindProperty("ConstantsList");
    }

    public override void OnInspectorGUI()
    {
        try
        {
            serializedObject.Update();

            if (constants.arraySize == 0)
            {
                EditorGUILayout.HelpBox("No Constants to Generate or Update", MessageType.Warning);
            }
            if (CacheExists == false && constants.arraySize != 0)
            {
                EditorGUILayout.HelpBox("Cache hasn't been generated. Press \"New Cache\" to generate one.", MessageType.Warning);
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("New Constant"))
            {
                constants.InsertArrayElementAtIndex(constants.arraySize - 1);
                scrollPosition.y = 0;
                changeLastEntryName = true;
                
            }
            EditorGUI.BeginDisabledGroup(constants.arraySize == 0);
            if (GUILayout.Button(CacheExists ? "Update Cache" : "New Cache"))
            {
                ProduceGlobalConstantsFile();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            string[] displayedOptions = new string[] { "Don't Allow", "Allow" };
            GUILayout.Label("Allow Update on Change");
            generateOnConstantListChangeAllowed = EditorGUILayout.Popup(generateOnConstantListChangeAllowed == true ? 1 : 0, displayedOptions) != 0;
            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();
            DisplayConstants();
            serializedObject.ApplyModifiedProperties();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void ProduceGlobalConstantsFile()
    {
        // TODO: Create a .cs class at the root of Assets using the 
        // Constants template.
        StringBuilder newFile = new StringBuilder();

        newFile.Append("namespace SharedData\n{");
        newFile.Append(ClassDefinition);
        newFile.Append("{\n\t");

        SerializedProperty constantName;
        SerializedProperty constantValue;
        SerializedProperty constantType;

        for (int i = 0; i < constants.arraySize; i++)
        {

            var currentElement = constants.GetArrayElementAtIndex(i);

            constantName = currentElement.FindPropertyRelative(StringPropertyName);
            constantValue = currentElement.FindPropertyRelative(StringPropertyValue);
            constantType = currentElement.FindPropertyRelative(StringPropertyType);

            var variableType = ((ConstantType)constantType.enumValueIndex).ToString().ToLower();
            var variableName = constantName.stringValue;
            var variableValue = ResolveType(constantType.enumValueIndex, constantValue.stringValue, out bool result);

            if (result == false) ResolveType(constantType.enumValueIndex, "0", out result);
            if (result == false)
            {
                Debug.Log("Failed to generate constants cache.");
                return;
            }
            newFile.Append(
                string.Format(ConstantVariableFormat, variableType, variableName, variableValue) +
                (i < constants.arraySize - 1 ? "\t" : string.Empty)
            );
        }

        newFile.Append("}}");

        // TODO: Create new .cs, and write to Asset folder
        using StreamWriter sw = new(Application.dataPath + ConstantCacheFile);
        sw.Write(newFile.ToString());

        AssetDatabase.Refresh();
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
        var autoGenerateCache = false;

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MaxHeight(MaxScrollHeight));
        for (int i = 0; i < constants.arraySize; i++)
        {
            var currentElement = constants.GetArrayElementAtIndex(i);

            SerializedProperty constantName = currentElement.FindPropertyRelative("Key");
            SerializedProperty constantValue = currentElement.FindPropertyRelative("Value");
            SerializedProperty constantType = currentElement.FindPropertyRelative("Type");

            if (changeLastEntryName == true && (i < constants.arraySize - 1) == false)
            {
                constantName.stringValue = $"NewConstant{constants.arraySize}";
                constantValue.stringValue = "0";
                constantType.enumValueIndex = (int)ConstantType.INT;
                changeLastEntryName = false;
                autoGenerateCache = generateOnConstantListChangeAllowed;
            }
            GUIContent buttonContent = new GUIContent("Remove", $"Remove {constantName.stringValue}");

            GUILayout.Space(2);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            EditorGUI.BeginDisabledGroup(i == 0);
            if(GUILayout.Button("▲"))
            {
                //TODO: Move Up
                constants.MoveArrayElement(i, i - 1);
                AttemptCacheGenerate();
                return;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(i == constants.arraySize - 1);
            if (GUILayout.Button("▼"))
            {
                //TODO: Move Down
                constants.MoveArrayElement(i, i + 1);
                AttemptCacheGenerate();
                return;
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndVertical();
            if (GUILayout.Button(buttonContent, GUILayout.Width(MaxWidth - 25), GUILayout.ExpandWidth(false), GUILayout.MaxHeight(40)))
            {
                int choice = promptOnConstantRemovalAllowed ? 
                    EditorUtility.DisplayDialogComplex("Removing Constant", 
                    $"Would you like to remove {constantName.stringValue} as a constant?", 
                    "Remove", 
                    "Cancel", 
                    "Remove and Never Prompt For This Session") : 0;
                if (choice != 1)
                {
                    if(choice == 2) promptOnConstantRemovalAllowed = false;
                    constants.DeleteArrayElementAtIndex(i);
                    AttemptCacheGenerate();
                    return;
                }
            }
            GUILayout.BeginVertical();
            GUILayout.Label(LabelID);
            constantName.stringValue = GUILayout.TextField(constantName.stringValue, GUILayout.MinWidth(MaxWidth), GUILayout.ExpandWidth(true));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label(LavelValue);
            constantValue.stringValue = GUILayout.TextField(constantValue.stringValue, GUILayout.MinWidth(MaxWidth), GUILayout.ExpandWidth(true));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label(LabelType);
            constantType.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup((ConstantType)constantType.enumValueIndex, GUILayout.MinWidth(MaxWidth / 2), GUILayout.ExpandWidth(true)));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
        }
        GUILayout.EndScrollView();
        if (autoGenerateCache == false) return;
        ProduceGlobalConstantsFile();
    }

    private void AttemptCacheGenerate()
    {
        if (generateOnConstantListChangeAllowed == false) return;
        ProduceGlobalConstantsFile();
    }
}
