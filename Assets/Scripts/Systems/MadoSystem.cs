using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static SharedData.Constants;

public sealed class MadoSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, Type[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize]{One, One, One, One},
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    readonly Mado[] MadoList = new Mado[]
    {
        new MDPyromado(),
        new MDCryomado(),
        new MDHyromado(), 
        new MDYamimado(), 
        new MDNichimado(),
        new MDTsukimado(),
        new MDYotsumado(), 
    };

    private BitArray Accessibility;
    private int _madoRefCount;

    protected override void OnInit()
    {
        Accessibility = new BitArray(MadoList.Length);

        //ToDO: Read Palyer data, and figure out what's unlocked
    }

    protected override void Main()
    {
        base.Main();

    }

    internal void EquipMadoToSlot(int index, int slotNumber)
    {
        if (Accessibility[index] == false) return;
        ((IActionCategory)this).AddItemToSlot(slotNumber, MadoList[index]);
    }

    internal void UnequipMadoFromSlot(int slotNumber)
    {
        ((IActionCategory)this).RemoveFromSlot(slotNumber);
    }

    internal T GetMado<T>() where T : Mado
    {
        return (T)ActionCategoryDetails.slots.Where(item => item.StaticItemType == typeof(T)).Single();
    }

    internal void IncreaseRefCount()
    {
        _madoRefCount++;
    }

    internal int GetRefCount()
    {
        return _madoRefCount;
    }

    internal void GainAccess(int index)
    {
        Accessibility[index] = true;
    }

    internal void Lock(int index)
    {
        Accessibility[index] = false;
    }
}
