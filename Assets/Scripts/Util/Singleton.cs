#nullable enable

using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    protected static T? Instance;
    public virtual Action? OnAwake { get; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)Convert.ChangeType(this, typeof(T));

            if (!transform.parent)
                DontDestroyOnLoad(this);
        }
        else
        {
            if (!transform.parent)
                Destroy(gameObject);
        }

        OnAwake?.Invoke();
    }

    public static bool IsNull => Instance == null;
}
