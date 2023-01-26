internal class ItemSystem : GameSystem, IActionCategory
{
    public IActionableItem[] Slots { get; set; } = new IActionableItem[IActionCategory.MaxSlots];

    protected override void OnInit()
    {

    }

    protected override void Main()
    {
        base.Main();
    }
}