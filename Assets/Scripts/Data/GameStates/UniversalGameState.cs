#nullable enable

using System;
using System.Diagnostics;
using UnityEditor;

[Serializable]
public sealed record UniversalGameState
{
    // Basic Data
    public string? identifier = "_new_game";

    public int flag;

    public UniversalGameState(int initValue)
    {
        flag = initValue;
    }

    public void ExecuteAtState(int stateTarget, Action? response)
    {
        if ((flag == stateTarget) == false) return;
        response?.Invoke();
    }

    public void Raise()
    {
        flag++;
        UnityEngine.Debug.Log($"Game State Raised: {flag - 1} >> {flag}");
    }

    public void Set(int value)
    {
        UnityEngine.Debug.Log($"Game State Flag Set: {flag} >> {value}");
        flag = value;
    }
}
