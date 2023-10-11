#nullable enable

using System;
using UnityEngine;

/// <summary>
/// A larger sized potion (still not very potent). Restores 1000 HP
/// </summary>
public sealed class ITPotionGrande : Item, IHealthModifier
{
    public override string ItemName => "Potion Grande";
    public override int ShopValue => 1000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITPotionGrande);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetHealthBonus => 1000;
    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    private IHealthModifier HealthModifier => this;

    private void TakePotion()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), HealthModifier.HealthBonus, true);
    }
}
