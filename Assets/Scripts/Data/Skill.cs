#nullable enable

using System;
using UnityEngine;


public abstract class Skill : IActionableItem
{
    protected PlayerEntity? Player { get; private set; }
    protected BossEntity? Boss { get; private set; }
    protected short? SkillID { get; private set; }

    public virtual string? SkillName { get; }
    public virtual ItemUseCallback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }

    public int SlotNumber { get; set; }

    public Skill()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            Player = GameManager.Player;
            Boss = GameManager.Boss;
            SkillID = (short)SkillSystem.GetRefCount();
            Debug.Log($"SkillID ({SkillID}) {{{SkillName}}}");
            SkillSystem.IncreaseRefCount();
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}
