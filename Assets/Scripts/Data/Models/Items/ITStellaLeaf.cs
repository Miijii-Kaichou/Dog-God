#nullable enable

using System;
using UnityEngine;
/// <summary>
/// An exceedingly rare leaf conjured by the very stars themselves.
/// Restores all your Health and Mana
/// </summary>
public sealed class ITStellaLeaf : Item, IHealthModifier, IManaModifier
{
    public override string? ItemName => "Stella Leaf";
    public override int ShopValue => 10000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITStellaLeaf);
    public override ItemUseCallback? OnActionUse => ConsumeLeaf;

    public float SetHealthBonus => 0;
    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public float SetManaBonus => 0;
    public BonusModificationType ManaModificationType => BonusModificationType.Whole;

    private void ConsumeLeaf()
    {
        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();
    }
}
