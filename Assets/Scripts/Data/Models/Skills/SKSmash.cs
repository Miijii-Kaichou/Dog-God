using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKSmash : Skill
{
    public override string SkillName => "Smash";
    public override Type StaticItemType => typeof(SKSmash);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
