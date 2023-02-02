#nullable enable

using System;
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
    public override Type? StaticItemType => typeof(ITMagusPotionOmega);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public float SetManaBonus => Random.Range(30f, 60f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }
    
    private void TakePotion()
    {
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();
        ManaSystem.SetMana(GameManager.Player.MaxManaValue * ((IManaModifier)this).ManaBonus, true);
    }
}
