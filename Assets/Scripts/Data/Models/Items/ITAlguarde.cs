#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// An elixir that enhances your defenses between
/// 5% and 20% for 30 seconds;
/// </summary>
public sealed class ITAlguarde : Item, IDefenseModifier, IUseLifeCycle
{
    public override string? ItemName => "Alguarde";
    public override Type? StaticItemType => typeof(ITAlguarde);
    public override ItemUseCallaback? OnActionUse => TakeElixir;

    public float SetDefenseBonus => Random.Range(5f, 20f);

    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30f;

    public float TickDuration => -EveryTick;

    public Action? OnLifeExpired => LoseDefenseBuff;

    private PlayerEntity? _playerEntity;
    int cachedStat = 0;

    private void LoseDefenseBuff()
    {
        // Return back to it's original stat before the change.
        _playerEntity!.stats[StatVariable.Defense] = cachedStat;
    }

    public Action? OnTick => throw new NotImplementedException();

    private void TakeElixir()
    {
        _playerEntity = GameManager.Player;

        cachedStat = _playerEntity.stats[StatVariable.Defense];

        // We only want to increase our defense between 5% and 20%.
        // No need for OnTick. Whatever percentage increase we get will
        // last for 30 seconds
        _playerEntity.stats[StatVariable.Defense] += 
            Mathf.RoundToInt((float)_playerEntity.stats[StatVariable.Defense] * 
            ((IDefenseModifier)this).DefenseBonus);
    }
}
