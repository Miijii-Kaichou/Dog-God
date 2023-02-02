using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKRailgun : Skill
{
    public override string SkillName => "Railgun";
    public override Type StaticItemType => typeof(SKRailgun);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
