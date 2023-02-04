#nullable enable

using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    protected static T Instance;
    public virtual Action? OnAwake { get; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)Convert.ChangeType(this, typeof(T));

            if (!transform.parent)
                DontDestroyOnLoad(this);

            OnAwake?.Invoke();
            return;
        }

        if (!transform.parent)
            Destroy(gameObject);

    }

    public static bool IsNull => Instance == null;
}
