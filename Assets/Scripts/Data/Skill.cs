#nullable enable

using System;
using UnityEngine;


public abstract class Skill : IActionableItem
{
    protected short? SkillID { get; private set; }

    public virtual string? SkillName { get; }
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }

    public int SlotNumber { get; set; }

    public Skill()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            var skillSystem = GameManager.GetSystem<SkillSystem>();
            SkillID = (short)skillSystem.GetRefCount();
            Debug.Log($"SkillID ({SkillID}) {{{SkillName}}}");
            skillSystem.IncreaseRefCount();
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}
