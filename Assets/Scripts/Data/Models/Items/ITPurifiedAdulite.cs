using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;
using System;

#nullable enable

/* It's Adulite, but completely purified. 
   Gain 3 levels as you restore all your MP and HP,
   whilst regenerating it between 8% and 12% for 3 whole minutes.*/
public sealed class ITPurifiedAdulite : Item ,IHealthModifier, IManaModifier, ILevelModifier, IUseLifeCycle
{
    public override string? ItemName => "Purified Adulite";
    public override Type? StaticItemType => typeof(ITPurifiedAdulite);
    public override ItemUseCallaback? OnActionUse => AbsorbAdulite;

    // We use the SetHealthBonus to know the value we want to work with
    public float SetHealthBonus => Random.Range(8, 12);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    // Same goes for the SetManaBonus
    public float SetManaBonus => Random.Range(8, 12);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public int LevelGain => 3;

    public PlayerEntity? _player { get; set; }

    public float LifeDuration => MinutesInSeconds * 3;
    public Action? OnLifeExpired => null;

    // We'll regenerate every seconds
    public float TickDuration => EveryTick;
    public Action? OnTick => Rejuvenate;

    public HealthSystem? HealthSystem { get; set; }
    public ManaSystem? ManaSystem { get; set; }
    public LevelingSystem? LevelSystem { get; set; }

    public void AbsorbAdulite()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();
        LevelSystem ??= GameManager.GetSystem<LevelingSystem>();

        _player ??= GameManager.Player;

        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();
        LevelSystem.UpTotalLevelsMore(LevelGain);

        ((IUseLifeCycle)this).BeginLifeCycle();
    }

    void Rejuvenate()
    {
        HealthSystem!.SetHealth(nameof(PlayerEntity), _player!.MaxHealthValue * ((IHealthModifier)this).HealthBonus, isRelative: true);
        ManaSystem!.SetMana(_player!.MaxManaValue * ((IManaModifier)this).ManaBonus, isRelative: true);
    }
}
