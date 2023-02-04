#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DOthella : Deity
{
    public override string? DeityName => "Othella";
    public override Type? StaticItemType => typeof(DOthella);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKKagami>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKOthello>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
