#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public class DKamui : Deity
{
    public override string? DeityName => "Kamui";
    public override Type? StaticItemType => typeof(DKamui);
    public override ItemUseCallback? OnActionUse => ActivateSkill;

    public override DeityType DeityType => DeityType.Normal;
    public override Skill? DivineSkill => SkillSystem.LocateSkill<SKBringForthTheGrandInferno>();
    public override Skill? DivineBlessing => SkillSystem.LocateSkill<SKLastingAmber>();

    private void ActivateSkill()
    {
        DivineSkill?.OnActionUse?.Invoke();
    }
}
