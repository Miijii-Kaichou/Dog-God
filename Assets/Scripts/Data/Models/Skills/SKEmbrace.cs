using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKEmbrace : Skill
{
    public override string SkillName => "Embrace";
    public override Type StaticItemType => typeof(SKEmbrace);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
