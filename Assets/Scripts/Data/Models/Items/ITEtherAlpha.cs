#nullable enable

using System;

/// <summary>
/// An even better kind of potion that really gets you spirits lifted.
/// Restores 1000 Health and Mana.
/// </summary>
public sealed class ITEtherAlpha : Item, IHealthModifier, IManaModifier
{
    public override string? ItemName => "Ether Alpha";
    public override Type? StaticItemType => typeof(ITEtherAlpha);
    public override ItemUseCallaback? OnActionUse => TakeEther;

    public float SetHealthBonus => 1000;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public HealthSystem? HealthSystem { get; set; }

    public float SetManaBonus => 1000;

    public BonusModificationType ManaModificationType => BonusModificationType.Whole;

    public ManaSystem? ManaSystem { get; set; }

    private void TakeEther()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();

        HealthSystem.SetHealth(nameof(PlayerEntity), ((IHealthModifier)this).HealthBonus, true);
        ManaSystem.SetMana(((IManaModifier)this).ManaBonus, true);
    }
}