#nullable enable
using System;

using static SharedData.Constants;


/// <summary>
/// A magus potion that's has long lasting effect. Regain 5% 
/// of you mana every second for 30 seconds total
/// </summary>
public sealed class ITMagusPotionDelta : Item, IManaModifier, IUseLifeCycle
{
    public override string ItemName => "Magus Potion Delta";
    public override Type? StaticItemType => typeof(ITMagusPotionDelta);
    public override ItemUseCallback? OnActionUse => TakePotion;

    public float SetManaBonus => 5f;

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public float LifeDuration => 30f;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => RegainMana;

    private IManaModifier ManaModifier => this;
    private IUseLifeCycle LifeExpectency => this;

    private void TakePotion()
    {
        LifeExpectency.Start();
    }

    private void RegainMana()
    {
        ManaSystem.SetMana(Player!.MaxManaValue * ManaModifier.ManaBonus, true);
    }
}
