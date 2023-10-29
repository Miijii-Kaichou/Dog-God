#nullable enable

using System;

[Serializable]
public sealed class DeitySystemState
{
    public DeitySystemState(int capacity)
    {
        MadeContract = new bool[capacity];
        SlotID = new int[capacity];
    }
    public bool[] MadeContract { get; }
    public int[] SlotID { get; }
}
