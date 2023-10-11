using Extensions;
using System;
using UnityEngine;

using static SharedData.Constants;

/// <summary>
/// Can only be successful if a good amount of trust is accumulated.
/// Completely lower her defense.
/// Use the skill: Embrace after 3 consecutive actions to win her over.
/// You can't hit her during dialogue. If hit,
/// Chatter and Charmer and all voided, and immediately enters Stage 3 if not so 
/// already
/// </summary>
public sealed class SKComfort : Skill, IAttackModifier, IDefenseModifier
{
    public override string SkillName => "Comfort";
    public override int ShopValue => 1999999;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKComfort);
    public override ItemUseCallback OnActionUse => UseSkill;

    public float SetAttackBonus => 1000f;
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    public int AttackCache { get; set; }

    public float SetDefenseBonus => Hundred;
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
