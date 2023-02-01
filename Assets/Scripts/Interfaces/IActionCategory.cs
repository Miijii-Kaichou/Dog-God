public interface IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, bool isExpensible) 
        ActionCategoryDetails { get; set; }

    public const int MaxSlots = 4;
    public void AddItemToSlot(int slotNumber, IActionableItem item)
    {
        item.SlotNumber = slotNumber;
        ActionCategoryDetails.slots[item.SlotNumber] = item;

        if (ActionCategoryDetails.quantities[item.SlotNumber] >= ActionCategoryDetails.capacities[item.SlotNumber])
            return;

        ActionCategoryDetails.quantities[item.SlotNumber]++;
    }
    public void UseActionItem(int slotNumber)
    {
        if (ActionCategoryDetails.quantities[slotNumber] == 0) return;
        if (ActionCategoryDetails.isExpensible) ActionCategoryDetails.quantities[slotNumber]--;
        ActionCategoryDetails.slots[slotNumber]?.UseAction();
    }
}