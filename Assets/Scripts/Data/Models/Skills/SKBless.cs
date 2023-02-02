using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBless : Skill
{
    public override string SkillName => "Bless";
    public override Type StaticItemType => typeof(SKBless);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
