#nullable enable

using System;
using UnityEngine;

public abstract class Item : IActionableItem, IShop
{
    protected PlayerEntity? Player => GameManager.Player;
    public short? ItemID { get; private set; }
    
    public virtual string? ItemName { get; }
    public virtual int ShopValue { get; } = 0;
    public virtual Sprite? ShopImage { get; }

    public virtual Type? StaticItemType { get; }  
    public virtual ItemUseCallback? OnActionUse { get; }
    public virtual int ItemEfficiency { get; }

    public int SlotNumber { get; set; }

    public bool EnabledIf => true;

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