using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKEngage : Skill
{
    public override string SkillName => "Engage";
    public override Type StaticItemType => typeof(SKEngage);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
