#nullable enable

using Extensions;
using System;
using UnityEngine;

using static SharedData.Constants;

/// <summary>
/// This is a Dark-Based Mado. Can be paired with the following skills:
/// <list type="bullet">
/// <item>Skill: Brace For Impact</item>
/// <item>Skill: Gluttony</item>
/// <item>Skill: Instincts</item>
/// <item>Skill: Railgun</item>
/// <item>Skill: Engage</item>
/// <item>Skill: Holy Prism</item>
/// <item>Othella's Divine Skill: Kagami</item>
/// <item>Patchouli's Divine Skill: Catastrophic Calamity Stream</item>
/// </list>
/// ---------------------------------------------------------------------------
/// <para>Normal Attacks increases by 50%</para>
/// <para>After successful parry, steal back up to 5% health and mana.</para>
/// <para>Reduce effectiveness of items</para>
/// </summary>
public sealed class MDYamimado : Mado, IAttackModifier, IDefenseModifier
{
    public override string MadoName => "Yamimado";
    public override Type StaticItemType => typeof(MDYamimado);
    public override ItemUseCallback OnActionUse => Infuse;

    public float SetDefenseBonus => 30f;

    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;

    public float SetAttackBonus => 50f;

    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;

    IAttackModifier? AttackModifier => this;
    IDefenseModifier? DefenseModifier => this;

    const int SkillEnhancementPercentage = 50;

    private void Infuse()
    {
        ApplyMadoEnhancements();
    }

    private void ApplyMadoEnhancements()
    {
        EnhancePlayerStatus();
        EnhanceSkills();
        ReduceItemEffectiveness();
    }

    private void EnhancePlayerStatus()
    {
        Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
        Player!.stats?[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifier!.DefenseBonus), DefenseModificationType);
    }

    private void EnhanceSkills()
    {
        SkillSystem.StackEnhancementForSkill<SKBraceForImpact>              (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKGluttony>                    (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKInstinct>                    (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKRailgun>                     (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKEngage>                      (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKKagami>                      (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKCatastrophicCalamityStream>  (SkillEnhancementPercentage);
    }

    private void ReduceItemEffectiveness()
    {
        ItemSystem.ReduceEffectivenessForAllItems(Two);
    }
}