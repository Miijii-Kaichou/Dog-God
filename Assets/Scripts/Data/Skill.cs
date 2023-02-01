using UnityEngine;

public abstract class Skill : MonoBehaviour, IRewardable, IActionableItem
{
    /*A skill can be of the following types;
        Transformation
        Mutation
        Emission
        Deduction
        
     There can also be skills that are given from a particular Weapon or Deity called their Divine Prowess.*/
    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();

    public string ItemName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Quantity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool AllowQuantityResize => throw new System.NotImplementedException();

    public int SlotNumber { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void UseItem()
    {
        throw new System.NotImplementedException();
    }
}
