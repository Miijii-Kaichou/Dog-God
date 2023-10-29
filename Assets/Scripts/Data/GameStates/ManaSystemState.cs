#nullable enable

using System;

[Serializable]
public sealed class ManaSystemState
{
    public ManaSystemState(float currentMana, float maxMana)
    {
        CurrentMana = currentMana;
        MaxMana = maxMana;
    }

    public float CurrentMana { get; }
    public float MaxMana { get; }
}
