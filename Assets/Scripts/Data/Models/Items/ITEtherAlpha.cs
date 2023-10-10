#nullable enable

using System;
using UnityEngine;

/// <summary>
/// An even better kind of potion that really gets you spirits lifted.
/// Restores 1000 Health and Mana.
/// </summary>
public sealed class ITEtherAlpha : Item, IHealthModifier, IManaModifier
{
    public override string? ItemName => "Ether Alpha";
    public override int ItemValue => 5000;
    public override Sprite? ItemImage => null;

    public override Type? StaticItemType => typeof(ITEtherAlpha);
    public override ItemUseCallback? OnActionUse => TakeEther;

    public float SetHealthBonus => 1000;
    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public float SetManaBonus => 1000;
    public BonusModificationType ManaModificationType => BonusModificationType.Whole;

    private IHealthModifier HealthModifier => this;
    private IManaModifier ManaModifier => this;

    private void TakeEther()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), HealthModifier.HealthBonus, true);
        ManaSystem.SetMana(ManaModifier.ManaBonus, true);
    }
}