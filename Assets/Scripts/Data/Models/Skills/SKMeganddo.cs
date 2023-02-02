using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKMeganddo : Skill
{
    public override string SkillName => "Meganddo";
    public override Type StaticItemType => typeof(SKMeganddo);
    public override ItemUseCallaback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
