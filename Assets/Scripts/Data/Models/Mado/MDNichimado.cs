#nullable enable

using Extensions;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// Can be acquired fusing Pyromado and Yamimado (Tsukimado True)
/// or Pyromado and Hyromado (Tsukimado Alter).
/// Skills influenced by Pyromado and Hyromado/Yamimado will be enhanced
/// ---------------------------------------------------------------------------
/// <para>Normal Attack increases by 60%</para>
/// <para>After successful parry, slows down attack rate of enemy (stackable up to 20%)</para>
/// <para>Defense increased by 30%</para>
/// <para>Effectiveness of items doubled.</para>
/// </summary>
public sealed class MDNichimado : Mado, IMadoFusion, IAttackModifier, IDefenseModifier, IHealthModifier, IManaModifier
{
    public override string? MadoName => "Nichimado";
    public override Type? StaticItemType => typeof(MDNichimado);
    public override ItemUseCallback? OnActionUse => Infuse;
    public override int MadoEnhancementValue => 50;

    // Need Main Component of 
    // Cryomado
    public MDPyromado? Pyromado { get; private set; }

    // Primary Component for a valid
    // Tsukimado is a
    // Hyromado
    public MDYamimado? JointMadoTrue { get; private set; }

    // Secondary Alternative Component for 
    // a valid Tsukimado is a
    // Yamimado
    public MDHyromado? JointMadoAlternative { get; private set; }

    public bool IsAlternative =>
    JointMadoTrue == null &&
    JointMadoAlternative != null;

    public float SetAttackBonus => 90f;
    public BonusModificationType AttackModificationType => BonusModificationType.PercentageOf;
    IAttackModifier? AttackModifier => this;

    public float SetDefenseBonus => 30f;
    public BonusModificationType DefenseModificationType => BonusModificationType.PercentageOf;
    IDefenseModifier? DefenseModifer => this;


    public float SetHealthBonus => Random.Range(1f, 10f);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;
    IHealthModifier? HealthModifier => this;

    public float SetManaBonus => Random.Range(1f, 10f);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;
    IManaModifier? ManaModifier => this;
    private void Infuse()
    {
        EnhancePlayerDefense();
        ApplySuccessfulParryCondition();
        ReduceItemEffectiveness();
        if (IsAlternative)
        {
            InfuseAlter();
            return;
        }
        InfuseTrue();
    }

    private void ReduceItemEffectiveness()
    {
        ItemSystem.ReduceEffectivenessForAllItems(Two);
    }

    private void ApplySuccessfulParryCondition()
    {
        AttackDefenseSystem.OnParrySuccess?.AddNewListener(() =>
        {
            HealthSystem.SetHealth(nameof(PlayerEntity), HealthModifier!.HealthBonus, true);
            ManaSystem.SetMana(ManaModifier!.ManaBonus, true);
            Player!.stats?[StatVariable.Attack].IncreaseThisBy(Mathf.RoundToInt(AttackModifier!.AttackBonus), AttackModificationType);
        }, true);
    }

    private void EnhancePlayerDefense()
    {
        Player!.stats?[StatVariable.Defense].IncreaseThisBy(Mathf.RoundToInt(DefenseModifer!.DefenseBonus), DefenseModificationType);
    }

    private void InfuseTrue()
    {
        Pyromado?.UseAction();
        JointMadoTrue?.UseAction();
    }

    private void InfuseAlter()
    {
        Pyromado?.UseAction();
        JointMadoAlternative?.UseAction();
    }

    // Essential Buffs
    // ...Goes Here...

    public void ValidateFusion(Mado[] additionalMado)
    {
        // We just need 2 mado for the fusion. Can't take
        // more than that.
        if (additionalMado.Length > Two) return;

        // Primary 
        var mainMado = additionalMado[Zero];

        // Validate if pairing mado is
        // Hyromado or Yamimado
        var pairingMado = additionalMado[One];

        // If our main mado is not Pyromado, invalid fusion
        if (mainMado.Is(Pyromado?.StaticItemType) == false) return;
        Pyromado = (MDPyromado)mainMado;

        // If our pairing mado isn't a Hyromado or Tsukimado
        // invalidate fusion
        if (mainMado.Is(JointMadoTrue?.StaticItemType))
            JointMadoTrue = (MDYamimado)pairingMado;

        if (pairingMado.Is(JointMadoAlternative?.StaticItemType))
            JointMadoAlternative = (MDHyromado)pairingMado;
    }
}