using System;
using static SharedData.Constants;

public class MadoSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, Type[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize]{One, One, One, One},
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    protected override void OnInit()
    {

    }

    protected override void Main()
    {
        base.Main();

    }
}
