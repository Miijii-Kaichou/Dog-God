using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>

public sealed class SKTalonite : Skill
{
    public override string SkillName => "Talonite";
    public override int ShopValue => 20000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKTalonite);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
