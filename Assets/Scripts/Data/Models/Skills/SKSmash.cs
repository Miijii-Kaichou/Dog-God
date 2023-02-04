using System;
using Random = UnityEngine.Random;
using static SharedData.Constants;
using Extensions;
using UnityEngine;

/// <summary>
/// A heavy swing downwards, increasing your damage
/// between 10% and 30%
/// </summary>
public sealed class SKSmash : Skill, IAttackModifier
{
    public override string SkillName => "Smash";
    public override Type StaticItemType => typeof(SKSmash);
    public override ItemUseCallback OnActionUse => UseSkill;

    public float SetAttackBonus => Random.Range(10f, 30f);
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    int _attachCache = 0;

    private void UseSkill()
    {
        _attachCache = Player.stats[StatVariable.Attack];
        Player.stats[StatVariable.Attack].IncreaseThisBy(_attachCache * Mathf.RoundToInt(((IAttackModifier)this).AttackBonus), AttackModificationType);
    }
}
