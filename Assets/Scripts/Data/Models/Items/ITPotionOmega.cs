#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// An enhanced version of the ALlpha version of a normal potion. Very potent.
/// So much in fact it'll restore 25% to 50% of your health
/// </summary>
public sealed class ITPotionOmega : Item, IHealthModifier
{
    public override string ItemName => "Potion Omega";
    public override int ShopValue => 2500;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITPotionOmega);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetHealthBonus => Random.Range(25, 50);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    IHealthModifier HealthModifier => this;

    private void TakePotion()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), Player!.MaxHealthValue * HealthModifier.HealthBonus, true);
    }
}