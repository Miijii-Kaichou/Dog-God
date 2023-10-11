using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKFury : Skill
{
    public override string SkillName => "Fury";
    public override int ShopValue => 40000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKFury);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
