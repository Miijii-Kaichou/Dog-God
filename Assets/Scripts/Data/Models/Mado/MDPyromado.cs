#nullable enable

using Extensions;
using System;
using UnityEngine;

using static SharedData.Constants;


/// <summary>
/// This is a Fire-Based Mado. Can be paired with the following skills:
/// <list type="bullet">
/// <item>Skill: Trifecta</item>
/// <item>Skill: Talonite</item>
/// <item>Skill: Eruption</item>
/// <item>Skill: Railgun</item>
/// <item>Skill: Fury (Blaze Runner)</item>
/// <item>Rosa's Divine Skill: "Shower Me in a Thousand Rose Petals"</item>
/// <item>Kamui's Divine Blessing: Lasting Amber</item>
/// <item>Othella's Divine Skill: Kagami</item>
/// </list>
/// ---------------------------------------------------------------------------
/// <para>Normal Attacks Increased by 10%</para>
/// <para>Becomes 20% on successful parry</para>
/// </summary>
public sealed class MDPyromado : Mado, IAttackModifier
{
    public override string MadoName => "Pyromado";
    public override Type StaticItemType => typeof(MDPyromado);
    public override ItemUseCallback OnActionUse => Infuse;

    public float SetAttackBonus => 10f;
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;

    IAttackModifier? AttackModifier => this;

    private const int SkillEnhancementPercentage = 50;

    private void Infuse()
    {
        ApplyMadoEnhancements();
    }

    private void ApplyMadoEnhancements()
    {
        EnhancePlayerAttack();
        EnhancePlayerAttackOnSuccessfulParry();
        EnhanceSkills();
    }

    private void EnhanceSkills()
    {
        SkillSystem.StackEnhancementForSkill<SKTrifecta>                        (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKTalonite>                        (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKEruption>                        (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKRailgun>                         (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKBlazeRunner>                     (30);
        SkillSystem.StackEnhancementForSkill<SKShowerMeInAThousandRosePetals>   (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKLastingAmber>                    (SkillEnhancementPercentage);
        SkillSystem.StackEnhancementForSkill<SKKagami>                          (SkillEnhancementPercentage);
    }

    private void EnhancePlayerAttack()
    {
        Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
    }

    private void EnhancePlayerAttackOnSuccessfulParry()
    {
        // Double Player's Attack after successful parry.
        // The AttackDefense system will remove the bonus after the initial hit after
        // the parry connects
        AttackDefenseSystem.SetSucessfulParryBonus(AttackModifier?.AttackBonus * 2f);
    }
}
