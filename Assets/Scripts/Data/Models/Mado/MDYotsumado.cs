using Extensions;
using System;
using UnityEngine;

using Array = System.Array;

/// <summary>
/// Can be acquired fusing Nichimado and Tsukimado. All mado benefits applies to all skills (except from Hazel's Divine Skill and Divine Blessing).
/// Normal Attack increased by 120%
/// After successful parry, steal back up to 20% health and mana, increase damage to 210%, and slow down attack rate of enemy (stackable up to 30%)
/// Defense increased by 60%
/// Effectiveness of items doubled!
/// </summary>
public sealed class MDYotsumado : Mado, IAttackModifier, IDefenseModifier
{
    public override string MadoName => "Yotsumado";
    public override Type StaticItemType => typeof(MDYotsumado);
    public override ItemUseCallaback OnActionUse => Infuse;
    public override int MadoEnhancementValue => 500;

    public float SetAttackBonus => 120f;

    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;

    public float SetDefenseBonus => 60f;

    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;

    private void Infuse()
    {
        GameManager.Player.stats[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(((IAttackModifier)this).AttackBonus), BonusModificationType.PercentageOf);
    }
}
