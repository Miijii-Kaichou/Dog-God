#nullable enable

// A normal potion that's not very potent. Restores 500 Hp
using System;
using UnityEngine;

public sealed class ITPotion : Item, IHealthModifier
{
    public override string? ItemName => "Potion";
    public override Type? StaticItemType => typeof(ITPotion);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetHealthBonus => 5;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    private IHealthModifier HealthModifier => this;

    public void TakePotion()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), HealthModifier.HealthBonus, isRelative: true);
    }
}