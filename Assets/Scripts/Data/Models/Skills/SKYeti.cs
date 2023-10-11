using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKYeti : Skill
{
    public override string SkillName => "Yeti";
    public override int ShopValue => 20000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKYeti);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
