using System;
using UnityEngine;

#nullable enable

public abstract class Skill : MonoBehaviour, IRewardable, IActionableItem
{
    public virtual string? SkillName { get; }
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }

    public int SlotNumber { get; set; }

    public void UseAction()
    {

        OnActionUse?.Invoke();
    }
}
