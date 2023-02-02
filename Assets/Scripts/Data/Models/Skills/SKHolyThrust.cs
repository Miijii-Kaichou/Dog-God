using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyThrust : Skill
{
    public override string SkillName => "Heal";
    public override Type StaticItemType => typeof(SKHeal);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
