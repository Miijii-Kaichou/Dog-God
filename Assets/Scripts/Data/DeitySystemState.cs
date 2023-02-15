#nullable enable

using System;

[Serializable]
public sealed class DeitySystemState
{
    public DeitySystemState(int capacity)
    {
        madeContract = new bool[capacity];
        slotID = new int[capacity];
    }
    public bool[] madeContract;
    public int[] slotID;
}
