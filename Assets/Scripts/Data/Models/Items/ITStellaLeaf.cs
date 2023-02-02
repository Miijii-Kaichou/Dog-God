#nullable enable

using System;
/// <summary>
/// An exceedingly rare leaf conjured by the very stars themselves.
/// Restores all your Health and Mana
/// </summary>
public sealed class ITStellaLeaf : Item, IHealthModifier, IManaModifier
{
    public override string? ItemName => "Stella Leaf";
    public override Type? StaticItemType => typeof(ITStellaLeaf);
    public override ItemUseCallaback? OnActionUse => ConsumeLeaf;

    public float SetHealthBonus => 0;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public HealthSystem? HealthSystem { get; set; }

    public float SetManaBonus => 0;

    public BonusModificationType ManaModificationType => BonusModificationType.Whole;

    public ManaSystem? ManaSystem { get; set; }

    private void ConsumeLeaf()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();

        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();
    }
}
