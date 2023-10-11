using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKMeganddo : Skill
{
    public override string SkillName => "Meganddo";
    public override int ShopValue => 100000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKMeganddo);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
