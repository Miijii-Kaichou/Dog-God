using static SharedData.Constants;

public abstract class Item : IActionableItem
{
    public virtual string? ItemName { get; }

    /*An item can be something that can heal you, replenish your mana, buff your stats, etc.
     
        You give a name of the item, the description, what stat or attribute it increase, or if
        it can be used on the enemy.*/

    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();

    public int Quantity { get; private set; }
    int IActionableItem.Quantity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool AllowQuantityResize => throw new System.NotImplementedException();

    public int SlotNumber { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void UseItem()
    {
        if (Quantity == 0) return;
        Quantity = Quantity > 0 ? Quantity-- : 0;
        OnItemUse?.Invoke();
    }

    public void IncreaseQuantity()
    {
        if (Quantity >= MaxQuantity) return;
        Quantity++;
    }
}
