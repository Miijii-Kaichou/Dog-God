#nullable enable

using System;

using Random = UnityEngine.Random;

/// <summary>
/// An enhanced version of a nomral magus potion. Restores between 10% and 30%
/// of you Mana.
/// </summary>
public sealed class ITMagusPotionAlpha : Item, IManaModifier
{
    public override string? ItemName => "Magus Potion Alpha";
    public override Type? StaticItemType => typeof(ITMagusPotionAlpha);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public ManaSystem? ManaSystem { get; set; }

    public float SetManaBonus => Random.Range(10f, 30f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    private void TakePotion()
    {
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();
        ManaSystem.SetMana(GameManager.Player.MaxManaValue * ((IManaModifier)this).ManaBonus, true);
    }
}
