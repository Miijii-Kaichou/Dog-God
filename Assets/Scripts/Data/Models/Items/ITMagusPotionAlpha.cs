#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// An enhanced version of a nomral magus potion. Restores between 10% and 30%
/// of you Mana.
/// </summary>
public sealed class ITMagusPotionAlpha : Item, IManaModifier
{
    public override string? ItemName => "Magus Potion Alpha";
    public override int ShopValue => 2500;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITMagusPotionAlpha);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetManaBonus => Random.Range(10f, 30f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    IManaModifier ManaModifier => this;

    private void TakePotion()
    {
        ManaSystem.SetMana(Player!.MaxManaValue * ManaModifier.ManaBonus, true);
    }
}
