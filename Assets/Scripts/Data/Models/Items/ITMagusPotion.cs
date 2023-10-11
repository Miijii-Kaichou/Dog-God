#nullable enable

using System;
using UnityEngine;

/// <summary>
/// A normal potion specifically made from magus shards. Restores 500 MP.
/// </summary>
public sealed class ITMagusPotion : Item, IManaModifier
{
    public override string? ItemName => "Magus Potion";
    public override int ShopValue => 500;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITMagusPotion);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetManaBonus => 500;
    public BonusModificationType ManaModificationType => BonusModificationType.Whole;
    
    private IManaModifier ManaModifier => this;

    private void TakePotion()
    {
        ManaSystem.SetMana(ManaModifier.ManaBonus, true);
    }
}
