using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolySmite : Skill
{
    public override string SkillName => "Holy Smite";
    public override int ShopValue => 500000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKHolySmite);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        
    }
}
