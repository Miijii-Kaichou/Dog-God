#nullable enable

using Extensions;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class MDTsukimado : Mado, IMadoFusion, IAttackModifier, IDefenseModifier, IHealthModifier, IManaModifier
{
    public override string? MadoName => "Tsukimado";
    public override Type? StaticItemType => typeof(MDTsukimado);
    public override ItemUseCallback? OnActionUse => Infuse;
    public override int MadoEnhancementValue => 50;

    // Need Main Component of 
    // Cryomado
    public MDCryomado? Cryomado { get; private set; }

    // Primary Component for a valid
    // Tsukimado is a
    // Hyromado
    public MDHyromado? JointMadoTrue { get; private set; }
    
    // Secondary Alternative Component for 
    // a valid Tsukimado is a
    // Yamimado
    public MDYamimado? JointMadoAlternative { get; private set; }

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
        Cryomado?.UseAction();
        JointMadoTrue?.UseAction();
    }

    private void InfuseAlter()
    {
        Cryomado?.UseAction();
        JointMadoAlternative?.UseAction();
    }

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

        // If our main mado is not Cryomado, invalid fusion
        if (mainMado.Is(Cryomado?.StaticItemType) == false) return;
        Cryomado = (MDCryomado)mainMado;

        // If our pairing mado isn't a Hyromado or Tsukimado
        // invalidate fusion
        if (pairingMado.Is(JointMadoTrue?.StaticItemType))
            JointMadoTrue = (MDHyromado)pairingMado;

        if (mainMado.Is(JointMadoAlternative?.StaticItemType))
            JointMadoAlternative = (MDYamimado)pairingMado;
    }


}
