#nullable enable

using Extensions;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

/// <summary>
/// Can be acquired fusing Nichimado and Tsukimado. All mado benefits applies to all skills (except from Hazel's Divine Skill and Divine Blessing).
/// Normal Attack increased by 120%
/// After successful parry, steal back up to 20% health and mana, increase damage to 210%, and slow down attack rate of enemy (stackable up to 30%)
/// Defense increased by 60%
/// Effectiveness of items doubled!
/// </summary>
public sealed class MDYotsumado : Mado, IAttackModifier, IDefenseModifier, IHealthModifier, IManaModifier
{
    public override string MadoName => "Yotsumado";
    public override Type StaticItemType => typeof(MDYotsumado);
    public override ItemUseCallback OnActionUse => Infuse;
    public override int MadoEnhancementValue => 500;

    public MDNichimado? Nichimado { get; private set; }
    public MDTsukimado? Tsukimado { get; private set; }

    public float SetAttackBonus => 120f;
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    private IAttackModifier? AttackModifier => this;

    public float SetDefenseBonus => 60f;
    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;
    private IDefenseModifier? DefenseModifier => this;

    public float SetHealthBonus => Random.Range(1f, 20f);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;
    private IHealthModifier? HealthModifier => this;

    public float SetManaBonus => Random.Range(1f, 20f);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;
    private IManaModifier? ManaModifier => this;

    private void Infuse()
    {
        Nichimado?.UseAction();
        Tsukimado?.UseAction();
        EnhancePlayerDefense();
        ApplySuccessfulParryCondition();
        Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
    }

    private void EnhancePlayerDefense()
    {
        Player!.stats?[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifier!.DefenseBonus), DefenseModificationType);
    }

    private void ApplySuccessfulParryCondition()
    {
        AttackDefenseSystem.OnParrySuccess?.AddNewListener(() =>
        {
            HealthSystem.SetHealth(nameof(PlayerEntity), HealthModifier!.HealthBonus, true);
            ManaSystem.SetMana(ManaModifier!.ManaBonus, true);
            Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
        }, true);
    }
}
