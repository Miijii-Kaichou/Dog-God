using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour, IRewardable, IActionableItem
{
    /*A skill can be of the following types;
        Transformation
        Mutation
        Emission
        Deduction
        
     There can also be skills that are given from a particular Weapon or Deity called their Divine Prowess.*/
    public string itemName { get; set; }
    public int Quantity { get; set; } = 0;
    public bool AllowQuantityResize { get; set; } = false;
    public int SlotNumber { get; set; } = -1;
    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();
}
