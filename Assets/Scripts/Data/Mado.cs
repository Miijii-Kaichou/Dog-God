using System;
using static SharedData.Constants;

#nullable enable

public class Mado : IActionableItem
{
    public virtual string MadoName { get; }
    public virtual ItemUseCallaback OnActionUse { get; }
    public virtual Type? StaticItemType { get; }
    
    public int SlotNumber { get; set; }


    public void UseAction()
    {

        OnActionUse?.Invoke();
    }
}

