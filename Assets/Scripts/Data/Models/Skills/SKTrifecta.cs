using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKTrifecta : Skill
{
    public override string SkillName => "Trifect";
    public override Type StaticItemType => typeof(SKTrifecta);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
