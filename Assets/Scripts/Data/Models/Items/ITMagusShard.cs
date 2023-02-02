#nullable enable

using System;
using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// A small fragment of a crystal made from aging magic.
/// Use to restore your MP between 2% and 5% every tick (1 second)
/// for 10 seconds
/// </summary>
public class ITMagusShard : Item, IManaModifier, IUseLifeCycle
{
    public override string? ItemName => "Magus Shard";
    public override Type? StaticItemType => typeof(ITMagusShard);
    public override ItemUseCallaback? OnActionUse => AbsorbShard;

    public float SetManaBonus => Random.Range(2f, 5f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }

    public float LifeDuration => 10f;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => RegainMana;

    private void RegainMana()
    {
        ManaSystem!.SetMana(GameManager.Player.MaxManaValue * ((IManaModifier)this).ManaBonus, true);
    }

    private void AbsorbShard()
    {
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();
        ((IUseLifeCycle)this).BeginLifeCycle();
    }
}
