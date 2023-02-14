using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

using Object = UnityEngine.Object;

[CustomEditor(typeof(XVNMLAsset))]
public class XVNMLAssetScriptableObjectInspector : Editor
{
    SerializedProperty xvnmlAssetProperty;

    private void OnEnable()
    {
        xvnmlAssetProperty = serializedObject.FindProperty("asset");
    }

    public override void OnInspectorGUI()
    {
        OnXVNMLInspectorGUI();
        
    }

    private void OnXVNMLInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(xvnmlAssetProperty, new GUIContent("XVNML File"), GUILayout.ExpandWidth(true));
        if(GUILayout.Button("Load"))
        {
            // TODO: Validate .xvnml file before parsing information
        }
        EditorGUILayout.EndHorizontal();
    }
}
