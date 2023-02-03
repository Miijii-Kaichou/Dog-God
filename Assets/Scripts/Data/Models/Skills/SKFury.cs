using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKFury : Skill
{
    public override string SkillName => "Fury";
    public override Type StaticItemType => typeof(SKFury);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
