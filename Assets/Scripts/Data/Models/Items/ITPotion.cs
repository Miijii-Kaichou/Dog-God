#nullable enable

// A normal potion that's not very potent. Restores 500 Hp
using System;
using UnityEngine;

public sealed class ITPotion : Item, IHealthModifier
{
    public override string? ItemName => "Potion";
    public override Type? StaticItemType => typeof(ITPotion);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public float SetHealthBonus => 5;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public HealthSystem? HealthSystem { get; set; }


    public void TakePotion()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        HealthSystem.SetHealth(nameof(PlayerEntity), ((IHealthModifier)this).HealthBonus, isRelative: true);
    }
}