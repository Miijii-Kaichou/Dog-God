using Extensions;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// Speak to Koko a lot better.
/// Know her at a personal level. Really lower her defenses.
/// Gain trust if charmer successful.
/// Get more dialogue and trust with Shopkeepers and Hazel 
/// at Heaven's Plaza.
/// </summary>
public sealed class SKCharmer : Skill, IAttackModifier, IDefenseModifier
{
    public override string SkillName => "Charmer";
    public override Type StaticItemType => typeof(SKCharmer);
    public override ItemUseCallback OnActionUse => UseSkill;

    public float SetAttackBonus => Random.Range(100f, 200f);
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    public int AttackCache { get; set; }

    public float SetDefenseBonus => 75f;
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
