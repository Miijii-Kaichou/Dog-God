#nullable enable

using System;
using static SharedData.Constants;

/// <summary>
/// 
/// </summary>
public sealed class SKInstinct : Skill, IUseLifeCycle
{
    public override string SkillName => "Instinct";
    public override Type StaticItemType => typeof(SKInstinct);
    public override ItemUseCallback OnActionUse => UseSkill;
    public override bool EnabledIf => Player.EntityStanceState == StanceState.Defensive;

    public float LifeDuration => 10f * EnhancementValue;
    public float TickDuration => -1;
    public Action? OnLifeExpired => null;
    public Action? OnTick => ListenForEnemyOnHit;

    private void ListenForEnemyOnHit()
    {
        Player.OnHealthNegativeChange += () =>
        {
            Player.EntityDefensiveState = DefensiveState.Parry;
            AttackDefenseSystem.OnParrySuccess.Trigger();
            // Leave Enemy Stunned for 10 seconds
        };
    }

    private IUseLifeCycle LifeExpectancy => this;

    private void UseSkill()
    {
        LifeExpectancy.Start();
    }
}
