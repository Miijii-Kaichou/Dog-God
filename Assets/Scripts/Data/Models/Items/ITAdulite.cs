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
public sealed  class ITAdulite : Item, IHealthModifier, IManaModifier, IUseLifeCycle
{
    private PlayerEntity? _player;

    public override string? ItemName => "Adulite";
    public override Type? StaticItemType => typeof(ITAdulite);
    public override ItemUseCallaback? OnActionUse => AbsorbAdulite;

    public float SetHealthBonus => Random.Range(5f, 10f);

    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public HealthSystem? HealthSystem { get; set; }

    public float SetManaBonus => Random.Range(5f, 10f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }

    public float LifeDuration => 30f;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => Rejuvenate;

    private void Rejuvenate()
    {
        HealthSystem!.SetHealth(nameof(PlayerEntity), _player.MaxHealthValue * ((IHealthModifier)this).HealthBonus);
        ManaSystem!.SetMana(_player.MaxManaValue * ((IManaModifier)this).ManaBonus);
    }

    private void AbsorbAdulite()
    {
        _player = GameManager.Player;
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();

        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();

        ((IUseLifeCycle)this).BeginLifeCycle();
    }
}
