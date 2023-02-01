using static SharedData.Constants;

internal class Mado : IActionableItem
{
    public int SlotNumber { get; set; }

    public ItemUseCallaback OnActionUse { get; }
}

