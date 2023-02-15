#nullable enable

using System;

[Serializable]
public sealed class ItemSystemState
{
    public ItemSystemState(int capacity)
    {
        quantity = new int[capacity];
        owned = new int[capacity];
        slotID = new int[capacity]; 
    }

    public int[] quantity;
    public int[] owned;
    public int[] slotID;
}
