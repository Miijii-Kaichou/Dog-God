#nullable enable
using System;

using static SharedData.Constants;

/// <summary>
/// A potion that's has long lasting effect. Regain 2% 
/// of you health every second for 30 seconds total
/// </summary>
public sealed class ITPotionDelta : Item, IHealthModifier, IUseLifeCycle
{
    public override string ItemName => "Potion Delta";
    public override Type? StaticItemType => typeof(ITPotionDelta);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public HealthSystem? HealthSystem { get; set; }

    public float SetHealthBonus => 2f;

    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => Recover;

    private void Recover()
    {
        HealthSystem!.SetHealth(nameof(PlayerEntity), GameManager.Player.MaxHealthValue * ((IHealthModifier)this).HealthBonus, true);
    }

    private void TakePotion()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        ((IUseLifeCycle)this).BeginLifeCycle();
    }
}
