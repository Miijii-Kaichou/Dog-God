#nullable enable

using System;
using System.Runtime;
using System.Runtime.CompilerServices;

/// <summary>
/// A special kind of potion that rejuvenates the soul.
/// Restores 500 Health and Mana
/// </summary>
public sealed class ITEther : Item, IHealthModifier, IManaModifier
{
    public override string? ItemName => "Ether";
    public override Type? StaticItemType => typeof(ITEther);
    public override ItemUseCallaback? OnActionUse => TakeEther;

    public float SetHealthBonus => 500;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public HealthSystem? HealthSystem { get; set; }

    public float SetManaBonus => 500;

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