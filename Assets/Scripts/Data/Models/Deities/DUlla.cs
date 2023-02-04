#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DUlla : Deity
{
    public override string? DeityName => "Ulla";
    public override Type? StaticItemType => typeof(DUlla);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKJackofaltrade>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKProdigy>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
