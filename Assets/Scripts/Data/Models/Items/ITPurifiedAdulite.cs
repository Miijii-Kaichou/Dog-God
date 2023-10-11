using Random = UnityEngine.Random;

using static SharedData.Constants;
using System;
using UnityEngine;

#nullable enable

/* It's Adulite, but completely purified. 
   Gain 3 levels as you restore all your MP and HP,
   whilst regenerating it between 8% and 12% for 3 whole minutes.*/
public sealed class ITPurifiedAdulite : Item, IHealthModifier, IManaModifier, ILevelModifier, IUseLifeCycle
{
    public override string? ItemName => "Purified Adulite";
    public override int ShopValue => 50000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITPurifiedAdulite);
    public override ItemUseCallback? OnActionUse => AbsorbAdulite;

    // We use the SetHealthBonus to know the value we want to work with
    public float SetHealthBonus => Random.Range(8, 12);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    // Same goes for the SetManaBonus
    public float SetManaBonus => Random.Range(8, 12);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public int LevelGain => 3;

    public float LifeDuration => MinutesInSeconds * 3;
    public Action? OnLifeExpired => null;

    // We'll regenerate every seconds
    public float TickDuration => EveryTick;
    public Action? OnTick => Rejuvenate;

    IUseLifeCycle? LifeExpectancy => this;
    IHealthModifier? HealthModifier => this;
    IManaModifier? ManaModifier => this;

    public void AbsorbAdulite()
    {
        HealthSystem.RestoreAllHealth(nameof(PlayerEntity));
        ManaSystem.RestoreAllMana();
        ExperienceSystem.UpTotalLevelsMore(LevelGain);

        LifeExpectancy?.Start();
    }

    void Rejuvenate()
    {
        HealthSystem.SetHealth(nameof(PlayerEntity), 
            Player!.MaxHealthValue * 
            HealthModifier!.HealthBonus, 
            isRelative: true);
        ManaSystem.SetMana(Player.MaxManaValue * ManaModifier!.ManaBonus, true);
    }
}