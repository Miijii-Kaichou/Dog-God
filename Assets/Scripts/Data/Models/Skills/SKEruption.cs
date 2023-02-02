using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKEruption : Skill
{
    public override string SkillName => "Eruption";
    public override Type StaticItemType => typeof(SKEruption);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
