using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyPrism : Skill
{
    public override string SkillName => "Holy Prism";
    public override Type StaticItemType => typeof(SKHolyPrism);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
