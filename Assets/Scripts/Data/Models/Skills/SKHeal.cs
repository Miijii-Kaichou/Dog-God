using System;
using Random = UnityEngine.Random;
using static SharedData.Constants;
using UnityEngine;

/// <summary>
/// Increase health between 10% and 25%
/// Enhance Skill with Yotsumado equipped.
/// </summary>
public sealed class SKHeal : Skill, IHealthModifier, IEnhanceWithMado<MDYotsumado>
{
    public override string SkillName => "Heal";
    public override int ShopValue => 10000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKHeal);
    public override ItemUseCallback OnActionUse => UseSkill;

    public float SetHealthBonus => Random.Range(10f, 25f);

    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public HealthSystem HealthSystem { get; set; }
    public MadoSystem MadoSystem { get; set; }

    public MDYotsumado MadoEnhancer { get; set; }

    private PlayerEntity _player;

    private void UseSkill()
    {
        _player = GameManager.Player;

        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        MadoSystem ??= GameManager.GetSystem<MadoSystem>();

        MadoEnhancer = MadoSystem.GetMado<MDYotsumado>();

        HealthSystem.SetHealth(nameof(PlayerEntity), _player.MaxHealthValue * 
            ((IHealthModifier)this).HealthBonus * 
            IncludeMadoEnhancementValues());
    }

    private float IncludeMadoEnhancementValues()
    {
        return MadoEnhancer.MadoEnhancementValue;
    }
}
