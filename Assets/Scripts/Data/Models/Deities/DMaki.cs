#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DMaki : Deity
{
    public override string? DeityName => "Maki";
    public override Type? StaticItemType => typeof(DMaki);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKDevoteYourSoul>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKVengence>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
