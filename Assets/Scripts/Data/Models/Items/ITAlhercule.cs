#nullable enable

using System;
using UnityEngine;
using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// An elixir that enhances your attack between
/// 5% and 20% for 30 seconds;
/// </summary>
public sealed class ITAlhercule : Item, IAttackModifier, IUseLifeCycle
{
    public override string? ItemName => "Alguarde";
    public override Type? StaticItemType => typeof(ITAlhercule);
    public override ItemUseCallaback? OnActionUse => TakeElixir;

    public float SetAttackBonus => Random.Range(5f, 20f);

    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30f;

    public float TickDuration => -EveryTick;

    public Action? OnLifeExpired => LoseAttackBuff;

    private PlayerEntity? _playerEntity;
    int cachedStat = 0;

    private void LoseAttackBuff()
    {
        // Return back to it's original stat before the change.
        _playerEntity!.stats[StatVariable.Attack] = cachedStat;
    }

    public Action? OnTick => throw new NotImplementedException();

    private void TakeElixir()
    {
        _playerEntity = GameManager.Player;

        cachedStat = _playerEntity.stats[StatVariable.Attack];

        // We only want to increase our defense between 5% and 20%.
        // No need for OnTick. Whatever percentage increase we get will
        // last for 30 seconds
        _playerEntity.stats[StatVariable.Attack] +=
            Mathf.RoundToInt((float)_playerEntity.stats[StatVariable.Attack] *
            ((IAttackModifier)this).AttackBonus);
    }
}
