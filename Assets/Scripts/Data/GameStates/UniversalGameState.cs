#nullable enable

using System;

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
    }

    public void Set(int value)
    {
        flag = value;
    }
}
