using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyThrust : Skill
{
    public override string SkillName => "Holy Thrust";
    public override int ShopValue => 500000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKHolyThrust);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        // High miss chance, but devastating blow if successful
    }
}
