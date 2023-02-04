using System;
using UnityEngine;

#nullable enable

public interface IActionCategory
{
    public (IActionableItem?[] slots, int[] quantities, int[] capacities, Type?[] requiredTypes, bool isExpensible) 
        ActionCategoryDetails { get; set; }

    public const int MaxSlots = 4;
    public void AddItemToSlot(int slotNumber, IActionableItem item)
    {
        if (ActionCategoryDetails.quantities[slotNumber] >= ActionCategoryDetails.capacities[slotNumber])
            return;

        if (ActionCategoryDetails.quantities[slotNumber] > 0 &&
            item.StaticItemType != ActionCategoryDetails.requiredTypes[slotNumber])
        {
            Debug.Log($"Required Type of this slot is {ActionCategoryDetails.requiredTypes[slotNumber]?.Name}");
        }

        item.SlotNumber = slotNumber;

        ActionCategoryDetails.slots[slotNumber] = item;

        if (ActionCategoryDetails.quantities[slotNumber] == 0)
        {
            ActionCategoryDetails.requiredTypes[slotNumber] = item.StaticItemType;    
        }
        ActionCategoryDetails.quantities[slotNumber]++;
    }
    public void UseActionItem(int slotNumber)
    {
        if (ActionCategoryDetails.quantities[slotNumber] == 0) return;
        if (ActionCategoryDetails.isExpensible) ActionCategoryDetails.quantities[slotNumber]--;
        ActionCategoryDetails.slots[slotNumber]?.UseAction();
    }

    void RemoveFromSlot(int slotNumber, int count = 1)
    {
        ActionCategoryDetails.slots[slotNumber] = null;
        ActionCategoryDetails.quantities[slotNumber] -= count;
        ActionCategoryDetails.quantities[slotNumber] = Mathf.Clamp(ActionCategoryDetails.quantities[slotNumber], 0,
            ActionCategoryDetails.capacities[slotNumber]);
        if (ActionCategoryDetails.quantities[slotNumber] == 0)
        {
            ActionCategoryDetails.requiredTypes[slotNumber] = null;
        }
    }
}