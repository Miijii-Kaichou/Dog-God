#nullable enable

using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBeelzebub : Skill
{
    public override string SkillName => "Beelzebub";
    public override int ShopValue => 666000;
    public override Sprite? ShopImage => null;

    public override Type StaticItemType => typeof(SKBeelzebub);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        throw new NotImplementedException();
    }
}
