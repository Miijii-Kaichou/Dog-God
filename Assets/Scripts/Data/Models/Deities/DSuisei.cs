#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DSuisei : Deity
{
    public override string? DeityName => "Suisei";
    public override Type? StaticItemType => typeof(DSuisei);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKSeptemmare>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKSirensLove>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
