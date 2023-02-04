#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class DAzulette : Deity
{
    public override string? DeityName => "Azulette";
    public override Type? StaticItemType => typeof(DAzulette);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKAzure>();
    public override Skill? DivineBlessing => null;

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
