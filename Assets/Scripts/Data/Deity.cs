using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Deity : MonoBehaviour, IActionableItem
{
    /*This class will include contain the name of the deity,
     the amount of Faith you must have in order to sign a contract with them,
     rather you've made a contract with them or not,
     they're HP, Mana, and Stats, as well as the skills that you can use
     once you have signed a contract.*/

    //The name of the weapon
    public string m_name;

    //The description or lore of the weapon.
    public string caption;

    //Weapon stats (these states will be added to the players)
    public EntityStats stats;
    public List<DamageStatVariable> damageProperties;

    //Requirements that the player has to have in order to wield it.
    //public Requirement[] requirements;

    //And the Deity's Emblem
    public string itemName { get; set; }
    public int Quantity { get; set; } = 0;
    public bool AllowQuantityResize { get; set; } = false;
    public int SlotNumber { get; set; } = -1;
    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();
}
