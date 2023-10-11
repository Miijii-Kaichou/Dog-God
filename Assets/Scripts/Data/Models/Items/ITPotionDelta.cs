#nullable enable
using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// A potion that's has long lasting effect. Regain 2% 
/// of you health every second for 30 seconds total
/// </summary>
public sealed class ITPotionDelta : Item, IHealthModifier, IUseLifeCycle
{
    public override string ItemName => "Potion Delta";
    public override int ShopValue => 2500;
    public override Sprite? ShopImage => null;

    public override Type? StaticItemType => typeof(ITPotionDelta);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public HealthSystem? HealthSystem { get; set; }

    public float SetHealthBonus => 2f;

    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => Recover;

    private IHealthModifier HealthModifier => this;
    private IUseLifeCycle LifeExpectancy => this;

    private void Recover()
    {
       HealthSystem.SetHealth(nameof(PlayerEntity), Player!.MaxHealthValue * HealthModifier.HealthBonus, true);
    }

    private void TakePotion()
    {
        LifeExpectancy.Start();
    }
}
