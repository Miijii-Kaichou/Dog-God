#nullable enable

using System;
using UnityEngine;

public abstract class Item : IActionableItem
{
    protected PlayerEntity? Player => GameManager.Player;
    public short? ItemID { get; private set; }
    
    public virtual string? ItemName { get; }
    public virtual Type? StaticItemType { get; }  
    public virtual ItemUseCallback? OnActionUse { get; }
    public virtual int ItemEfficiency { get; }

    public int SlotNumber { get; set; }

    public Item()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            ItemID = (short)ItemSystem.GetRefCount();
            Debug.Log($"ItemID ({ItemID}) {{{ItemName}}}");
            ItemSystem.IncreaseRefCount();
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}