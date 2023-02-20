using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PoolItemDependencyContainer : MonoBehaviour
{
    bool isInitialized;
    
    [SerializeField]
    private Component[] poolItemDependencies;
    private Dictionary<Type, Component> DependencyDictionary;

    internal Component this[Type type] => Get(type);

    private void Awake() => Init();
    private void OnEnable() => Init();
    
    Component Get<T>(T Type) where T : Type
    {
        if (!isInitialized) Init();
        return DependencyDictionary[Type];
    }

    private void Init()
    {
        if (isInitialized) return;
        DependencyDictionary = new Dictionary<Type, Component>();
        foreach (var item in poolItemDependencies)
            DependencyDictionary.Add(item.GetType(), item);
        isInitialized = DependencyDictionary.Count != 0;
    }

    internal bool Contains(Type type)
    {
        if (!isInitialized) Init();
        return DependencyDictionary.ContainsKey(type);
    }
}
