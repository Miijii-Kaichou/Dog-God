using System;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public abstract class Deity : MonoBehaviour, IActionableItem
{

    public virtual string? DeityName { get; set; }
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }
    
    public int SlotNumber { get; set; }


    //The description or lore of the deity
    public string caption;

    //Deity stats (these states will be added to the players)
    public EntityStats stats;
    public List<DamageStatVariable> damageProperties;


    public void UseAction()
    {

        OnActionUse?.Invoke();
    }
}
