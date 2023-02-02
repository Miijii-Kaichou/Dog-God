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
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public float SetManaBonus => 5f;

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }

    public float LifeDuration => 30f;

    public float TickDuration => EveryTick;

    public Action? OnLifeExpired => null;

    public Action? OnTick => RegainMana;

    private void TakePotion()
    {
        ManaSystem = GameManager.GetSystem<ManaSystem>();
        ((IUseLifeCycle)this).BeginLifeCycle();
    }

    private void RegainMana()
    {
        ManaSystem!.SetMana(GameManager.Player.MaxManaValue * ((IManaModifier)this).ManaBonus, true);
    }
}
