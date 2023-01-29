public delegate void ItemUseCallaback();

#nullable enable
public interface IActionableItem
{
    public string? itemName { get; set; }
    public int Quantity { get; set; }
    public bool AllowQuantityResize { get; }
    public int SlotNumber { get; set; }
    public ItemUseCallaback OnItemUse { get; }
    const int MaxQuantity = 4;

    public void Use()
    {
        if (Quantity == 0) return;
        Quantity = Quantity > 0 ? Quantity-- : 0;
        OnItemUse?.Invoke();
    }

    public void IncreaseQuantity()
    {
        if (AllowQuantityResize && Quantity != 0) return;
        if (Quantity >= MaxQuantity) return;
        Quantity++;
    }
}