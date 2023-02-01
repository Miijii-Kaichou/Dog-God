using System.Collections.Generic;
using UnityEngine;

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
    public ItemUseCallaback OnUse => throw new System.NotImplementedException();

    public string ItemName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Quantity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool AllowQuantityResize => throw new System.NotImplementedException();

    public int SlotNumber { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();

    public void UseItem()
    {
        throw new System.NotImplementedException();
    }
}
