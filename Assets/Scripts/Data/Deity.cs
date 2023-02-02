using System;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public abstract class Deity : IActionableItem
{

    public virtual string? DeityName { get; set; }
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }
    
    public int SlotNumber { get; set; }


    //The description or lore of the deity
    public string caption;

    //Deity stats (these states will be added to the players)
    public EntityStats stats;

    public void UseAction()
    {

        OnActionUse?.Invoke();
    }
}
