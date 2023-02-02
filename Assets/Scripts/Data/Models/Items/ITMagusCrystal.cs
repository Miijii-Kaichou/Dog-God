#nullable enable

using System;
using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// A crystal made from aging magic. Use to restore your Mana 2% to 5% every
/// tick for 1 whole minute;
/// </summary>
public sealed class ITMagusCrystal : Item, IManaModifier, IUseLifeCycle
{
    public override string? ItemName => "Magus Shard";
    public override Type? StaticItemType => typeof(ITMagusCrystal);
    public override ItemUseCallaback? OnActionUse => AbsorbCrystal;

    public float SetManaBonus => Random.Range(2f, 5f);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }

    public float LifeDuration => (float)MinutesInSeconds;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => RegainMana;

    private void RegainMana()
    {
        ManaSystem!.SetMana(((IManaModifier)this).ManaBonus, true);
    }

    private void AbsorbCrystal()
    {
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();
        ((IUseLifeCycle)this).BeginLifeCycle();
    }
}

