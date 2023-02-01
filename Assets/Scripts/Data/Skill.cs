using UnityEngine;

#nullable enable

public abstract class Skill : MonoBehaviour, IRewardable, IActionableItem
{
    public virtual ItemUseCallaback? OnActionUse { get; }

    public virtual string? SkillName { get; }
    public int SlotNumber { get; set; }
}
