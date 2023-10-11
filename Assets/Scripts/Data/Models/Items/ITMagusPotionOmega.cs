#nullable enable

using System;
using UnityEngine;
using Random =  UnityEngine.Random;

/// <summary>
/// An enhanced version of an alpha magus potion.
/// Magus Crystals are polished and cured between making this 
/// to assure you gain between 30% and 60%
/// of your Mana.
/// </summary>
public sealed class ITMagusPotionOmega : Item, IManaModifier
{
    public override string? ItemName => "Magus Potion Omega";
    public override int ShopValue => 2500;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITMagusPotionOmega);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetManaBonus => Random.Range(30f, 60f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    private IManaModifier ManaModifier => this;
    
    private void TakePotion()
    {
        ManaSystem.SetMana(Player!.MaxManaValue * ManaModifier.ManaBonus, true);
    }
}
