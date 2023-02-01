using static SharedData.Constants;

internal class ItemSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize]
        {
            MaxQuantity,
            MaxQuantity,
            MaxQuantity,
            MaxQuantity
        },
        isExpensible = true
    };

    protected override void OnInit()
    {

    }

    protected override void Main()
    {
        base.Main();
    }
}