using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKYeti : Skill
{
    public override string SkillName => "Yeti";
    public override Type StaticItemType => typeof(SKYeti);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
