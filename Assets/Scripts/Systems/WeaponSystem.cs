public class WeaponSystem : GameSystem, IActionCategory
{
    public IActionableItem[] Slots { get; set; } = new IActionableItem[IActionCategory.MaxSlots];
}
