#nullable enable
using System;
using UnityEngine;

public abstract class Item : IActionableItem
{
    public short? ItemID { get; private set; }
    
    public virtual string? ItemName { get; }
    public virtual Type? StaticItemType { get; }  
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual int ItemEfficiency { get; }

    public int SlotNumber { get; set; }

    public Item()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            var itemSystem = GameManager.GetSystem<ItemSystem>();
            ItemID = (short)itemSystem.GetRefCount();
            Debug.Log($"ItemID ({ItemID}) {{{ItemName}}}");
            itemSystem.IncreaseRefCount();
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}