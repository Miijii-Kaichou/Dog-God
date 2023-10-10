#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;
using Extensions;

/// <summary>
/// An elixir that enhances your defenses between
/// 5% and 20% for 30 seconds;
/// </summary>
public sealed class ITAlguarde : Item, IDefenseModifier, IUseLifeCycle
{
    public override string? ItemName => "Alguarde";
    public override int ItemValue => 10000;
    public override Sprite? ItemImage => null;

    public override Type? StaticItemType => typeof(ITAlguarde);
    public override ItemUseCallback? OnActionUse => TakeElixir;

    public float SetDefenseBonus => Random.Range(5f, 20f);

    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30f;

    public float TickDuration => -EveryTick;

    public Action? OnLifeExpired => LoseDefenseBuff;

    int cachedStat = 0;

    private IDefenseModifier DefenseModifier => this;
    private IUseLifeCycle LifeExpectancy => this;

    private void LoseDefenseBuff()
    {
        // Return back to it's original stat before the change.
        Player!.stats![StatVariable.Defense] = cachedStat;
    }

    public Action? OnTick => throw new NotImplementedException();

    private void TakeElixir()
    {
        cachedStat = Player!.stats![StatVariable.Defense];

        // We only want to increase our defense between 5% and 20%.
        // No need for OnTick. Whatever percentage increase we get will
        // last for 30 seconds
        Player.stats[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifier.DefenseBonus), DefenseModificationType);

        LifeExpectancy.Start();
    }
}
