using System;
using static SharedData.Constants;

internal class ItemSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, Type[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
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
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = true
    };
    public Type[] RequiredTypes { get; }

    RuntimeActionSystem _runtimeActionSystem;

    protected override void OnInit()
    {

    }

    protected override void Main()
    {
        base.Main();
    }
}