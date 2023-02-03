using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKEmbrace : Skill
{
    public override string SkillName => "Embrace";
    public override Type StaticItemType => typeof(SKEmbrace);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
