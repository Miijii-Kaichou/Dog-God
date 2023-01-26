using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.EditorTools;
using UnityEngine;

public class ConstantsManager : MonoBehaviour
{
    [SerializeField] Constant[] test = new Constant[4];

}
[Serializable]
public sealed class Constant
{
    public string Key;
    public string Value;
    public ConstantType Type;
}
public enum ConstantType
{
    INT,
    FLOAT,
    DOUBLE,
    STRING
}

