using Extensions;
using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// Speak to Koko.
/// Learn a bit about her and lower her defenses.
/// You can't hit her during dialogue. If hit, her attack
/// will increase by 50%.
/// Gain trust if skill was successful.
/// Having this skill enables you to interact with Shopkeepers 
/// and Hazel at Heaven's Plaza
/// </summary>
public sealed class SKChatter : Skill, IAttackModifier, IDefenseModifier
{
    public override string SkillName => "Chatter";
    public override Type StaticItemType => typeof(SKChatter);
    public override ItemUseCallback OnActionUse => UseSkill;

    public float SetAttackBonus => 50f;
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    public int AttackCache { get; set; }

    public float SetDefenseBonus => 25f;
    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;
    public int DefenseCache { get; set; }

    // This skill is a unique one. We're not amplfying our own
    // attack, but the Boss's (which is pretty wild).
    // And it also checks if she gets hit during the use of it.
    // Keep this in mind.

    private void UseSkill()
    {
        // TODO: Start talking to Koko
        // Listen for end of dialogue.
        // She will also not attack until the
        // OnHealthNegativeChange is invoked

        DefenseCache = Boss.stats[StatVariable.Defense];
        Boss.stats[StatVariable.Defense].DecreaseThisBy(DefenseCache * Mathf.RoundToInt(((IDefenseModifier)this).DefenseBonus), DefenseModificationType);
        Boss.OnHealthNegativeChange += () =>
        {
            RestoreDefense();
            Aggro();
        };
    }

    private void Aggro()
    {
        AttackCache = Boss.stats[StatVariable.Attack];
        Boss.stats[StatVariable.Attack].IncreaseThisBy(AttackCache * Mathf.RoundToInt(((IAttackModifier)this).AttackBonus), AttackModificationType);
    }

    // Restore the Boss's Defenses
    private void RestoreDefense()
    {
        Boss.stats[StatVariable.Defense] = DefenseCache;
    }
}
