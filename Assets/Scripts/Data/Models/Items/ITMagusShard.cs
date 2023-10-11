#nullable enable

using System;
using Random = UnityEngine.Random;

using static SharedData.Constants;
using UnityEngine;

/// <summary>
/// A small fragment of a crystal made from aging magic.
/// Use to restore your MP between 2% and 5% every tick (1 second)
/// for 10 seconds
/// </summary>
public sealed class ITMagusShard : Item, IManaModifier, IUseLifeCycle
{
    public override string? ItemName => "Magus Shard";
    public override int ShopValue => 10000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITMagusShard);
    public override ItemUseCallback? OnActionUse => AbsorbShard;

    public float SetManaBonus => Random.Range(2f, 5f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }

    public float LifeDuration => 10f;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => RegainMana;

    private IManaModifier ManaModifier => this;
    private IUseLifeCycle LifeExpectency => this;

    private void RegainMana()
    {
        ManaSystem.SetMana(Player!.MaxManaValue * ManaModifier.ManaBonus, true);
    }

    private void AbsorbShard()
    {
        LifeExpectency.Start();
    }
}
