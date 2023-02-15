#nullable enable

using System;

[Serializable]
public sealed record UniversalGameState
{
    public int value;

    public UniversalGameState(int initValue)
    {
        this.value = initValue;
    }

    public void ExecuteAtState(int stateTarget, Action? response)
    {
        if ((value == stateTarget) == false) return;
        response?.Invoke();
    }

    public void Raise()
    {
        value++;
    }

    public void Set(int value)
    {
        this.value = value;
    }
}
