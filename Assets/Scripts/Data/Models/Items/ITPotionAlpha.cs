#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// An enhanced version of a normal potion. Potent enough to restore
/// between 10% and 15% of your health
/// </summary>
public sealed class ITPotionAlpha : Item, IHealthModifier
{
    public override string ItemName => "Potion Alpha";
    public override int ItemValue => 2500;
    public override Sprite? ItemImage => null;

    public override Type? StaticItemType => typeof(ITPotionAlpha);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetHealthBonus => Random.Range(10, 15);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    IHealthModifier HealthModifier => this;

    private void TakePotion()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), Player!.MaxHealthValue * HealthModifier.HealthBonus, true);
    }
}
