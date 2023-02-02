using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBlazeRunner : Skill
{
    public override string SkillName => "Blaze Runner";
    public override Type StaticItemType => typeof(SKBlazeRunner);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
