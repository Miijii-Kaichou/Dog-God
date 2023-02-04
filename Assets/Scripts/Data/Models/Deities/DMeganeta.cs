#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DMeganeta : Deity
{
    public override string? DeityName => "Meganeta";
    public override Type? StaticItemType => typeof(DMeganeta);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKAzure>();
    public override Skill? DivineBlessing => null;

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
