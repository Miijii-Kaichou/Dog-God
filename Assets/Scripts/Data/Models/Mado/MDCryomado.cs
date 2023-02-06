#nullable enable

using Extensions;
using System;
using UnityEngine;

using static SharedData.Constants;


/// <summary>
/// This is a Holy (Light)-Based Mado. Can be paired with the following skills:
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
/// <para>Normal Attacks increases by 50%</para>
/// <para>All Skills Listed is Enhanced by 50%</para>
/// <para>Defense increases by 30%</para>
/// <para>Item Effectiveness Doubles</para>
/// </summary>
public sealed class MDCryomado : Mado, IAttackModifier
{
    public override string? MadoName => "Cryomado";
    public override Type? StaticItemType => typeof(MDCryomado);
    public override ItemUseCallback? OnActionUse => Infuse;

    public float SetAttackBonus => throw new NotImplementedException();
    public BonusModificationType AttackModificationType => throw new NotImplementedException();

    IAttackModifier? AttackModifier => this;

    private void Infuse()
    {
        EnhancePlayerAttack();
        ApplySlowDownInfliction();
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
        //InflictSystem.SlowDownAttackRate(1)
    }
}
