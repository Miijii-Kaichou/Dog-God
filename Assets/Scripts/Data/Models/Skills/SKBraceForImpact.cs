using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBraceForImpact : Skill
{
    public override string SkillName => "Brace For Impact";
    public override Type StaticItemType => typeof(SKBraceForImpact);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
