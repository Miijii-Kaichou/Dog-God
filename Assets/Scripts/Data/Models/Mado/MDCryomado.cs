#nullable enable

using Extensions;
using System;
using UnityEngine;

using static SharedData.Constants;

/// <summary>
/// This is a Ice-Based Mado. Can be paired with the following skills:
/// <list type="bullet">
/// <item>Skill: Yeti</item>
/// <item>Skill: Blizzard</item>
/// <item>Skill: Railgun</item>
/// <item>Suisei's Divine Skill: Seven Seas</item>
/// <item>Suisei's Divine Blessing: Siren's Love</item>
/// </list>
/// ---------------------------------------------------------------------------
/// <para>Normal Attack increases by 10%</para>
/// <para>On successful parry, slows down attack rate of enemy (is stackable up to 10% slowdown)</para>
/// </summary>
public sealed class MDCryomado : Mado, IAttackModifier
{
    public override string? MadoName => "Cryomado";
    public override Type? StaticItemType => typeof(MDCryomado);
    public override ItemUseCallback? OnActionUse => Infuse;
    public override int MadoEnhancementValue => 50;

    public float SetAttackBonus => throw new NotImplementedException();
    public BonusModificationType AttackModificationType => throw new NotImplementedException();

    IAttackModifier? AttackModifier => this;

    private void Infuse()
    {
        EnhancePlayerAttack();
        EnhanceSkills();
        ApplySlowDownInfliction();
    }

    private void EnhanceSkills()
    {
        SkillSystem.StackEnhancementForSkill<SKYeti>        (MadoEnhancementValue);
        SkillSystem.StackEnhancementForSkill<SKBlizzard>    (30);
        SkillSystem.StackEnhancementForSkill<SKRailgun>     (MadoEnhancementValue);
        SkillSystem.StackEnhancementForSkill<SKSeptemmare>  (MadoEnhancementValue);
        SkillSystem.StackEnhancementForSkill<SKSirensLove>  (MadoEnhancementValue);
    }

    private void EnhancePlayerAttack()
    {
        // Double Player's Attack after successful parry.
        // The AttackDefense system will remove the bonus after the initial hit after
        // the parry connects
        Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
    }

    private void ApplySlowDownInfliction()
    {
        AttackDefenseSystem.SetInflictAfterParrySuccess(InflictType.Slowdown, 0.1f);
    }
}