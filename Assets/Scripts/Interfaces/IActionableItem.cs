using System;

public delegate void ItemUseCallaback();

#nullable enable
public interface IActionableItem
{
    public int SlotNumber { get; set; }
    public abstract ItemUseCallaback? OnActionUse { get; }
    public abstract Type? StaticItemType { get; }
    void UseAction();
}