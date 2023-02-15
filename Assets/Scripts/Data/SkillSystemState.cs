#nullable enable

using System;

[Serializable]
public sealed class SkillSystemState
{
    public SkillSystemState(int capacity) 
    { 
        skillAcquired = new bool[capacity];
        slotID = new int[capacity];
    }

    public bool[] skillAcquired;
    public int[] slotID;
}
