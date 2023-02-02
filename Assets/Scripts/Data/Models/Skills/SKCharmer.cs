using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKCharmer : Skill
{
    public override string SkillName => "Charmer";
    public override Type StaticItemType => typeof(SKCharmer);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
