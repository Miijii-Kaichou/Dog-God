#nullable enable

using System;
using Random = UnityEngine.Random;
using static SharedData.Constants;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public sealed class SKGluttony : Skill, IHealthModifier, IManaModifier
{
    public override string SkillName => "Gluttony";
    public override int ShopValue => 50000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKHeal);
    public override ItemUseCallback OnActionUse => UseSkill;

    public float SetHealthBonus => Random.Range(2f, 5f);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;
    private IHealthModifier? HealthModifier => this;

    public float SetManaBonus => Random.Range(2f, 5f);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;
    private IManaModifier? ManaModifier => this;

    private void UseSkill()
    {
        HealthSystem.SetHealth(PlayerEntityTag, Player!.HealthValue * HealthModifier!.HealthBonus, true);
        ManaSystem.SetMana(Player!.ManaValue * ManaModifier!.ManaBonus, true);
    }
}
