using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKHolyFlare : Skill
{
    public override string SkillName => "Holy Flare";
    public override int ShopValue => 500000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKHolyFlare);
    public override ItemUseCallback OnActionUse => UseSkill;

    private void UseSkill()
    {
       // On Success, stuns enemy for 15 seconds
       // while life drains over time
       // Time doubles if Hyromado equipped

       // Damage is tripled
       // High Mana Consumption
    }
}
