#nullable enable

using System;
using UnityEngine;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKBraceForImpact : Skill, IUseLifeCycle
{
    public override string SkillName => "Brace For Impact";
    public override int ShopValue => 100000;
    public override Sprite? ShopImage => null;
    public override Type StaticItemType => typeof(SKBraceForImpact);
    public override ItemUseCallback OnActionUse => UseSkill;
    public override bool EnabledIf => Player?.EntityStanceState == StanceState.Defensive;

    public float LifeDuration => 10f * EnhancementValue;
    public float TickDuration => -1;
    public Action? OnLifeExpired => null;
    public Action? OnTick => null;

    private IUseLifeCycle LifeExpectancy => this;

    private void UseSkill()
    {
        LifeExpectancy.Start();
    }
}
