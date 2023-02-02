#nullable enable

using System;

/// <summary>
/// A normal potion specifically made from magus shards. Restores 500 MP.
/// </summary>
public sealed class ITMagusPotion : Item, IManaModifier
{
    public override string? ItemName => "Magus Potion";
    public override Type? StaticItemType => typeof(ITMagusPotion);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public ManaSystem? ManaSystem { get; set; }

    public float SetManaBonus => 500;

    public BonusModificationType ManaModificationType => BonusModificationType.Whole;
    
    private void TakePotion()
    {
        ManaSystem = GameManager.GetSystem<ManaSystem>();
        ManaSystem.SetMana(((IManaModifier)this).ManaBonus, true);
    }
}
