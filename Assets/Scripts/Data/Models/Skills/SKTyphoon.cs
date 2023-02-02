using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKTyphoon : Skill
{
    public override string SkillName => "Typhoon";
    public override Type StaticItemType => typeof(SKTyphoon);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
