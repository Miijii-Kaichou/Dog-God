using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBlazeRunner : Skill
{
    public override string SkillName => "Blaze Runner";
    public override int ShopValue => 40000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKBlazeRunner);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
