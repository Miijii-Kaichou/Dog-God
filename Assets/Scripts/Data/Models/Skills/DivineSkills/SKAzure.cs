#nullable enable

using Extensions;
using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// A blue light emits from you, making you feel lighter.
/// Heighten Attack and Defense for 1 whole minute.
/// Enhance skill with Yotsumado equipped.
/// </summary>
public sealed class SKAzure : Skill, IDivineSkill, IAttackModifier, IDefenseModifier, IAgilityModifier, IUseLifeCycle, IEnhanceWithMado<MDYotsumado>
{
    public override string? SkillName => "Azure's Divine Skill: Azure";
    public override Type? StaticItemType => typeof(SKAzure);
    public override ItemUseCallback? OnActionUse => UseSkill;

    public Deity? Deity => DeitySystem.LocateDeity<DAzulette>(); 
    public Action? OnSkillActivation { get; set; } = null;
    public Action? OnSkillDeactivation { get; set; } = null;
   
    public MDYotsumado MadoEnhancer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public float SetAttackBonus => 125f;
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    public float AttackCacheValue { get; set; }

    public float SetDefenseBonus => 125f;
    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;
    public float DefenseCacheValue { get; set; }

    public float SetAgilityBonus => 125f;
    public BonusModificationType AgilityModificationType => BonusModificationType.PercentageOf;
    public float AgilityCacheValue { get; set; }

    public float LifeDuration => MinutesInSeconds;
    public float TickDuration => -1;
    public Action? OnLifeExpired => RemoveSkillBuffs;
    public Action? OnTick => null;

    IAttackModifier AttackModifier => this;
    IDefenseModifier DefenseModifier => this;
    IAgilityModifier AgilityModifier => this;

    private void UseSkill()
    {
        if (Player == null) return;

        // Apply Attack, Agility and Defense bonues
        Player.stats![StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier.AttackBonus), AttackModificationType);
        Player.stats[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifier.DefenseBonus), DefenseModificationType);
        Player.stats[StatVariable.Agility].IncreaseThisBy(Mathf.RoundToInt(AgilityModifier.AgilityBonus), AgilityModificationType);

        // Begin Life Cycle
        ((IUseLifeCycle)this).Start();
    }

    private void RemoveSkillBuffs()
    {
        if (Player == null) return;

        // Remove Attack, Defense, and 
        Player.stats![StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier.AttackBonus), AttackModificationType);
        Player.stats[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifier.DefenseBonus), DefenseModificationType);
        Player.stats[StatVariable.Agility].IncreaseThisBy(Mathf.RoundToInt(AgilityModifier.AgilityBonus), AgilityModificationType);
    }
}
