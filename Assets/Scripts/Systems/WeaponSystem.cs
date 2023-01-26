using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : GameSystem, IActionCategory
{
    public IActionableItem[] Slots { get; set; } = new IActionableItem[IActionCategory.MaxSlots];
}
