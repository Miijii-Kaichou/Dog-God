using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>

public sealed class SKTalonite : Skill
{
    public override string SkillName => "Talonite";
    public override Type StaticItemType => typeof(SKTalonite);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
