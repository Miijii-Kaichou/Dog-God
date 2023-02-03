using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKUltraheal : Skill
{
    public override string SkillName => "Ultraheal";
    public override Type StaticItemType => typeof(SKUltraheal);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
