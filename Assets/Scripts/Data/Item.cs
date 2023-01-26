public abstract class Item : IActionableItem
{
    /*An item can be something that can heal you, replenish your mana, buff your stats, etc.
     
        You give a name of the item, the description, what stat or attribute it increase, or if
        it can be used on the enemy.*/

    public string itemName { get; set; }
    public int Quantity { get; set; } = 0;
    public bool AllowQuantityResize => true;
    public int SlotNumber { get; set; } = -1;
    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();
}
