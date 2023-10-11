#nullable enable

using System;
using Random = UnityEngine.Random;

using static SharedData.Constants;
using UnityEngine;

/// <summary>
/// A crystal made from aging magic. Use to restore your Mana 2% to 5% every
/// tick for 1 whole minute;
/// </summary>
public sealed class ITMagusCrystal : Item, IManaModifier, IUseLifeCycle
{
    public override string? ItemName => "Magus Shard";
    public override int ShopValue => 30000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITMagusCrystal);
    public override ItemUseCallback? OnActionUse => AbsorbCrystal;

    public float SetManaBonus => Random.Range(2f, 5f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => (float)MinutesInSeconds;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => RegainMana;

    IManaModifier ManaModifier => this;
    IUseLifeCycle LifeExpectency => this;

    private void RegainMana()
    {
        ManaSystem.SetMana(ManaModifier.ManaBonus, true);
    }

    private void AbsorbCrystal()
    {
        LifeExpectency.Start();
    }
}

