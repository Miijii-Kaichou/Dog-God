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
    public override ItemUseCallback? OnActionUse => TakeEther;

    public float SetHealthBonus => 500;
    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    public float SetManaBonus => 500;
    public BonusModificationType ManaModificationType => BonusModificationType.Whole;

    private IHealthModifier HealthModifier => this;
    private IManaModifier ManaModifier => this;

    private void TakeEther()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), HealthModifier.HealthBonus, true);
        ManaSystem.SetMana(ManaModifier.ManaBonus, true);
    }
}