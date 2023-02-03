using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKInstinct : Skill
{
    public override string SkillName => "Instinct";
    public override Type StaticItemType => typeof(SKInstinct);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
