﻿#nullable enable

using System;
using UnityEngine;

public abstract class Deity : IActionableItem
{
    protected short? DeityID { get; private set; }

    public virtual string? DeityName { get; set; }
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }
    public virtual DeityType DeityType { get; }
    public virtual Skill? DivineSkill { get; }
    public virtual Skill? DivineBlessing { get; }
    public virtual string? Caption { get; }
    public virtual EntityStats? Stats { get; }

    public int SlotNumber { get; set; }


    public Deity()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            var deitySystem = GameManager.GetSystem<DeitySystem>();
            DeityID = (short)deitySystem.GetRefCount();
            Debug.Log($"DeityID ({DeityID}) {{{DeityName}}}: " +
                $"Deity is {{{Enum.GetName(typeof(DeityType), DeityType)}}}");
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}
