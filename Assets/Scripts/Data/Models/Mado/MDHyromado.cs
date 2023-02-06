#nullable enable

using Extensions;
using System;
using UnityEngine;

using static SharedData.Constants;

/// <summary>
/// This is a Holy (Light)-Based Mado. Can be paired with the following skills:
/// <list type="bullet">
/// <item>Skill: Fury(becomes Typhoon)</item>
/// <item>Skill: Holy Smite</item>
/// <item>Skill: Holy Thrust</item>
/// <item>Skill: Holy Flare</item>
/// <item>Skill: Railgun</item>
/// <item>Skill: Holy Prism</item>
/// <item>Rosa's Divine Skill: "Shower Me in a Thousand Rose Petals"</item>
/// <item>Othella's Divine Skill: Kagami</item>
/// <item>Patchouli's Divine Skill: Catastrophic Calamity Stream</item>
/// <item>Ryuga's Divine Blessing: Sterling</item>
/// </list>
/// ---------------------------------------------------------------------------
/// <para>Normal Attacks increases by 50%</para>
/// <para>All Skills Listed is Enhanced by 50%</para>
/// <para>Defense increases by 30%</para>
/// <para>Item Effectiveness Doubles</para>
/// </summary>
public sealed class MDHyromado : Mado, IAttackModifier, IDefenseModifier
{
    public override string MadoName => "Hyromado";
    public override Type StaticItemType => typeof(MDHyromado);
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
        EnhanceItemEffectiveness();
    }

    private void EnhancePlayerStatus()
    {
        Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
        Player!.stats?[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifier!.DefenseBonus), DefenseModificationType);
    }

    private void EnhanceSkills()
    {
        SkillSystem.StackEnhancementForSkill<SKTyphoon>                         (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKHolySmite>                       (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKHolyThrust>                      (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKHolyFlare>                       (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKRailgun>                         (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKHolyPrism>                       (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKShowerMeInAThousandRosePetals>   (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKKagami>                          (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKCatastrophicCalamityStream>      (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKSterling>                        (SkillEnhancementPercentage);
    }

    private void EnhanceItemEffectiveness()
    {
        ItemSystem.StackEnhancementForAllItems(Two);
    }
}