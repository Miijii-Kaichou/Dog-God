using static SharedData.Constants;

public class SkillSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize] { One, One, One, One },
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
