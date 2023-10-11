using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKUltraheal : Skill
{
    public override string SkillName => "Ultraheal";
    public override int ShopValue => 50000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKUltraheal);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
