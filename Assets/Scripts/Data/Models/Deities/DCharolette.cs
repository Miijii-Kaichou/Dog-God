#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DCharolette : Deity
{
    public override string? DeityName => "Charolette";
    public override Type? StaticItemType => typeof(DCharolette);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKAzure>();
    public override Skill? DivineBlessing => null;

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}

