using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyThrust : Skill
{
    public override string SkillName => "Holy Thrust";
    public override Type StaticItemType => typeof(SKHolyThrust);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
