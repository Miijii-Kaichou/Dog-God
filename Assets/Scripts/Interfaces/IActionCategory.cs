public interface IActionCategory
{
    public IActionableItem[] Slots { get; set; }
    public const int MaxSlots = 4;
    public void AddItemToSlot(int slotNumber, IActionableItem item)
    {
        item.SlotNumber = slotNumber;
        Slots[slotNumber].Quantity++;
    }
    public void UseActionItem(int slotNumber) => Slots[slotNumber]?.Use();
}