using System;
using static SharedData.Constants;

/// <summary>
/// Increase health between 10% and 25%
/// Enhance Skill with Yotsumado equipped.
/// </summary>
public sealed class SKHeal : Skill, IHealthModifier, IEnhanceWithMado<MDYotsumado>
{
    public override string SkillName => "Heal";
    public override Type StaticItemType => typeof(SKHeal);
    public override ItemUseCallaback OnActionUse => UseSkill;

    public float SetHealthBonus => throw new NotImplementedException();

    public BonusModificationType HealthModificationType => throw new NotImplementedException();

    public HealthSystem HealthSystem { get; set; }
    public MDYotsumado MadoEnhancer { get; set; }

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
