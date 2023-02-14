using TMPro;
using UnityEditor;
using UnityEngine;
using XVNML.Core.Tags;
using XVNML.XVNMLUtility;

public class XVNMLModule : MonoBehaviour
{
    // XVNML File Target

    public XVNMLAsset xvnmlFile;

    public TagBase Root { get; private set; }

    public void Awake()
    {
        GenerateXVNMLDocumentData();
    }

    private void GenerateXVNMLDocumentData()
    {
        var @object = XVNMLObj.Create(xvnmlFile.filePath);
        if (@object == null)
        {
            Debug.LogError("XVNML Object creation has failed. Please chech the log for details");
            return;
        }

        Debug.Log("XVNML Object created successfully");
        Root = @object.Root;
    }
}