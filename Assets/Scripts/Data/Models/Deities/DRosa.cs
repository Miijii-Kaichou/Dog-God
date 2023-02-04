#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DRosa : Deity
{
    public override string? DeityName => "Rosa";
    public override Type? StaticItemType => typeof(DRosa);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKShowerMeInAThousandRosePetals>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKDamselinrede>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
