using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBlizzard : Skill
{
    public override string SkillName => "Blizzard";
    public override int ShopValue => 40000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKBlizzard);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
