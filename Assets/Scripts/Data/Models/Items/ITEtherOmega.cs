#nullable enable
using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A highly enhanced version of Ether Alpha. Restores
/// between 25% and 50% of both your Health and Mana
/// </summary>
public sealed class ITEtherOmega : Item, IHealthModifier, IManaModifier
{
    public override string ItemName => "Ether Omega";
    public override int ItemValue => 5000;
    public override Sprite? ItemImage => null;

    public override Type? StaticItemType => typeof(ITEtherOmega);
    public override ItemUseCallback? OnActionUse => TakeEther;

    public float SetHealthBonus => Random.Range(25, 50);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public float SetManaBonus => Random.Range(25, 50);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    IHealthModifier HealthModifier => this;
    IManaModifier ManaModifier => this;

    void TakeEther()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), Player!.MaxManaValue * HealthModifier.HealthBonus, true);
        ManaSystem.SetMana(Player!.MaxManaValue * ManaModifier.ManaBonus, true);
    }
}
