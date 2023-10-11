using System;
using UnityEngine;

using static SharedData.Constants;

/// <summary>
/// A 3-hit combo move that forms a triangle.
/// Amplified if Pyromado is equipped
/// </summary>
public sealed class SKTrifecta : Skill
{
    public override string SkillName => "Trifecta";
    public override int ShopValue => 35000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKTrifecta);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
        Debug.Log($"Used {SkillName}");
    }
}
