#nullable enable

using System;

[Serializable]
public sealed class MadoSystemState
{
    public MadoSystemState(int capacity)
    {
        accessibility = new bool[capacity];
        slotID = new int[capacity];
    }
    public bool[] accessibility;
    public int[] slotID;
}
