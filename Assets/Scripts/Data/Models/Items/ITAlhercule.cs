#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;
using Extensions;

/// <summary>
/// An elixir that enhances your attack between
/// 5% and 20% for 30 seconds;
/// </summary>
public sealed class ITAlhercule : Item, IAttackModifier, IUseLifeCycle
{
    public override string? ItemName => "Alhercule";
    public override int ShopValue => 10000;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITAlhercule);
    public override ItemUseCallback? OnActionUse => TakeElixir;

    public float SetAttackBonus => Random.Range(5f, 20f);

    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30f;

    public float TickDuration => -EveryTick;

    public Action? OnLifeExpired => LoseAttackBuff;

    int cachedStat = 0;

    IUseLifeCycle LifeExpectancy => this;
    IAttackModifier AttackModifer => this;

    private void LoseAttackBuff()
    {
        // Return back to it's original stat before the change.
        Player!.stats![StatVariable.Attack] = cachedStat;
    }

    public Action? OnTick => throw new NotImplementedException();

    private void TakeElixir()
    {
        cachedStat = Player!.stats![StatVariable.Attack];

        // We only want to increase our defense between 5% and 20%.
        // No need for OnTick. Whatever percentage increase we get will
        // last for 30 seconds
        Player.stats[StatVariable.Attack].IncreaseThisBy(
            Mathf.RoundToInt(AttackModifer.AttackBonus), AttackModificationType);

        LifeExpectancy.Start();
    }
}
