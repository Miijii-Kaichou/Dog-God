using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBlizzard : Skill
{
    public override string SkillName => "Blizzard";
    public override Type StaticItemType => typeof(SKBlizzard);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
