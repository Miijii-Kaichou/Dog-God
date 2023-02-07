using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyThrust : Skill
{
    public override string SkillName => "Holy Thrust";
    public override Type StaticItemType => typeof(SKHolyThrust);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        // High miss chance, but devastating blow if successful
    }
}
