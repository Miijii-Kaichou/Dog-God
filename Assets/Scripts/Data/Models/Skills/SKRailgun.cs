using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKRailgun : Skill
{
    public override string SkillName => "Railgun";
    public override int ShopValue => 250000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKRailgun);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
