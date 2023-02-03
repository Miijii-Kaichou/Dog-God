#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// It's Adulite, but has a lot of absorbed energy. Very close to it's purest state.
/// Restores all your Health and Mana, and regenerates them between 8% and 12% every tick
/// for 1 whole minute!
/// </summary>
public sealed class ITChargedAdulite : Item, IHealthModifier, IManaModifier, IUseLifeCycle
{
    public override string? ItemName => "Charged Adulite";
    public override Type? StaticItemType => typeof(ITChargedAdulite);
    public override ItemUseCallback? OnActionUse => AbsorbAdulite;


    // We use the SetHealthBonus to know the value we want to work with
    public float SetHealthBonus => Random.Range(8, 12);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    // Same goes for the SetManaBonus
    public float SetManaBonus => Random.Range(8, 12);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public int LevelGain => 3;

    public float LifeDuration => MinutesInSeconds;
    public Action? OnLifeExpired => null;

    // We'll regenerate every seconds
    public float TickDuration => EveryTick;
    public Action? OnTick => Rejuvenate;

    public HealthSystem? HealthSystem { get; set; }
    public ManaSystem? ManaSystem { get; set; }

    IHealthModifier HealthModifier => this;
    IManaModifier ManaModifier => this;
    IUseLifeCycle LifeExpectancy => this;

    public void AbsorbAdulite()
    {
        // Begin Item Effect Life Cycle
        LifeExpectancy.Start();

        // Restore all HP and MP on Use
        // Then increase the player's level up 3
        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();
    }

    void Rejuvenate()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), Player!.MaxHealthValue * HealthModifier.HealthBonus, isRelative: true);
        ManaSystem.SetMana(Player.MaxManaValue * ManaModifier.ManaBonus, isRelative: true);
    }
}