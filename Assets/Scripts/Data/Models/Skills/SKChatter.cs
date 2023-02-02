using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKChatter : Skill
{
    public override string SkillName => "Chatter";
    public override Type StaticItemType => typeof(SKChatter);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
