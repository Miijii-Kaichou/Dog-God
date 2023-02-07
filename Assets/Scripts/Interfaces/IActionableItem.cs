using System;

public delegate void ItemUseCallback();

#nullable enable
public interface IActionableItem
{
    public int SlotNumber { get; set; }
    public abstract ItemUseCallback? OnActionUse { get; }
    public abstract Type? StaticItemType { get; }
    public abstract bool EnabledIf { get; }
    void UseAction();
}