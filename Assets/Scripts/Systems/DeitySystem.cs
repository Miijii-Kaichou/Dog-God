using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeitySystem : GameSystem, IActionCategory
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
