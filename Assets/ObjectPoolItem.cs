using System;
using UnityEngine;

[Serializable]
public class ObjectPoolItem
{
    public string name;
    public int size;
    public GameObject prefab;
    public bool expandPool;
}