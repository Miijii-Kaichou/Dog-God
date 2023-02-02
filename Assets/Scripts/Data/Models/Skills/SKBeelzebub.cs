using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBeelzebub : Skill
{
    public override string SkillName => "SKBeelzebub";
    public override Type StaticItemType => typeof(SKBeelzebub);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
