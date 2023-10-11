#nullable enable

using System;
using UnityEngine;

public abstract class Deity : IActionableItem, IShop
{
    protected short? DeityID { get; private set; }

    public virtual string? DeityName { get; set; }
    public virtual int ShopValue { get; } = 0;
    public virtual Sprite? ShopImage { get; }

    public virtual ItemUseCallback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }
    public virtual DeityType DeityType { get; }
    public virtual Skill? DivineSkill { get; }
    public virtual Skill? DivineBlessing { get; }
    public virtual string? Caption { get; }
    public virtual EntityStats? Stats { get; }

    public int SlotNumber { get; set; }

    public bool EnabledIf => true;

    public Deity()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            DeityID = (short)DeitySystem.GetRefCount();
            Debug.Log($"DeityID ({DeityID}) {{{DeityName}}}: " +
                $"Deity is {{{Enum.GetName(typeof(DeityType), DeityType)}}}");
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}
