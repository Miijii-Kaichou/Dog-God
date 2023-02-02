using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolySmite : Skill
{
    public override string SkillName => "Holy Smite";
    public override Type StaticItemType => typeof(SKHolySmite);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
