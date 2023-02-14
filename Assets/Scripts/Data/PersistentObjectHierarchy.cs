using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Persistant Object Management System
/// Any object will the PersistantObject script will not be destoyed on load
/// And will be referenced with this object
/// </summary>
public static class PersistentObjectHierarchy
{
    static Dictionary<string, GameObject> objectIdDictionary = new Dictionary<string, GameObject>();
    public static string GetID(GameObject obj) => GenerateID(obj);

    static string GenerateID(GameObject obj)
    {
        string tag = "";
        while (true)
        {
            var genID = 1000 + objectIdDictionary.Count;
            tag = obj.gameObject.name + genID;
            if (!objectIdDictionary.ContainsKey(tag) || objectIdDictionary.Count == 0)
            {
                objectIdDictionary.Add(tag, obj);
                return tag;
            }
        }
    }

    public static void PrintDictionary()
    {
        foreach (KeyValuePair<string, GameObject> kvp in objectIdDictionary)
        {
            Debug.Log($"ObjectRef: {kvp.Value}; InstanceID: {kvp.Key}");
        }
    }

    /// <summary>
    /// Will get all objects similar to the one passed
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool OneExists(GameObject pObj)
    {
        var _object = from obj in objectIdDictionary where obj.Key.Contains(pObj.gameObject.name) select obj;
        return _object.Count() > 0;
    }

    internal static GameObject Find(string name)
    {
        return objectIdDictionary.Where(k => k.Key.Contains(name)).Select(k => k.Value).FirstOrDefault();
    }
}