using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKTyphoon : Skill
{
    public override string SkillName => "Typhoon";
    public override int ShopValue => 40000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKTyphoon);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
