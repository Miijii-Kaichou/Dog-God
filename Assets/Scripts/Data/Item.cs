using System;
using static SharedData.Constants;
#nullable enable

public abstract class Item : IActionableItem
{
    public virtual string? ItemName { get; }

    public virtual Type? StaticItemType { get; }

    /*An item can be something that can heal you, replenish your mana, buff your stats, etc.
     
        You give a name of the item, the description, what stat or attribute it increase, or if
        it can be used on the enemy.*/

    public virtual ItemUseCallaback? OnActionUse { get; }

    public int SlotNumber { get; set; }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}
