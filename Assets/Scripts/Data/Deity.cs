using System.Collections.Generic;
using UnityEngine;

#nullable enable

public abstract class Deity : MonoBehaviour, IActionableItem
{
    /*This class will include contain the name of the deity,
     the amount of Faith you must have in order to sign a contract with them,
     rather you've made a contract with them or not,
     they're HP, Mana, and Stats, as well as the skills that you can use
     once you have signed a contract.*/

    //The name of the weapon
    public string? DeityName { get; set; }
    
    //The description or lore of the deity
    public string caption;

    //Deity stats (these states will be added to the players)
    public EntityStats stats;
    public List<DamageStatVariable> damageProperties;

    //Requirements that the player has to have in order to wield it.
    //public Requirement[] requirements;

    public int SlotNumber { get; set; }

    public virtual ItemUseCallaback? OnActionUse { get; }
}
