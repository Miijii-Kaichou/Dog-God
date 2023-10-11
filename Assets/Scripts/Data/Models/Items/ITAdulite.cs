#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// The holiest material there is. Perhaps more valuable than a Stella Leaf.
/// Restores all your Health and Mana, and restore it between 5% and 10% every
/// tick for 30 seconds;
/// </summary>
public sealed class ITAdulite : Item, IHealthModifier, IManaModifier, IUseLifeCycle
{
    public override string? ItemName => "Adulite";
    public override int ShopValue => 10000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITAdulite);
    public override ItemUseCallback? OnActionUse => AbsorbAdulite;

    public float SetHealthBonus => Random.Range(5f, 10f);

    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public float SetManaBonus => Random.Range(5f, 10f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30f;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => Rejuvenate;

    private IHealthModifier HealthModifier => this;
    private IManaModifier ManaModifier => this;
    private IUseLifeCycle LifeExpectency => this;

    private void Rejuvenate()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), Player!.MaxHealthValue * HealthModifier.HealthBonus);
        ManaSystem.SetMana(Player.MaxManaValue * ManaModifier.ManaBonus);
    }

    private void AbsorbAdulite()
    {
        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();

        LifeExpectency.Start();
    }
}
