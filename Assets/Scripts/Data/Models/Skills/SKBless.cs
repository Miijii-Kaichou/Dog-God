using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBless : Skill
{
    public override string SkillName => "Bless";
    public override int ShopValue => 777000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKBless);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
