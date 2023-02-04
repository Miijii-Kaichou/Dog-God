#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DHazel : Deity
{
    public override string? DeityName => "Hazel Alonoire";
    public override Type? StaticItemType => typeof(DHazel);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKYouAreNotAlone>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKICanNotImagineAWorldWithoutYou>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
