using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyPrism : Skill
{
    public override string SkillName => "Holy Prism";
    public override int ShopValue => 1000000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKHolyPrism);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
