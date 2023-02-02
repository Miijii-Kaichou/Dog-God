using System;
using System.Collections;
using System.Linq;
using static SharedData.Constants;

public class DeitySystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, Type[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize] { One, One, One, One },
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    private readonly Deity[] DeityList =
    {

    };

    private BitArray Accessibility;
    private int _skillRefCount;

    protected override void OnInit()
    {
        Accessibility = new BitArray(DeityList.Length);
    }

    internal T GetSkill<T>() where T : Skill
    {
        return (T)ActionCategoryDetails.slots.Where(skill => skill.StaticItemType == typeof(T)).Single();
    }

    internal int GetRefCount()
    {
        return _skillRefCount;
    }

    internal void IncreaseRefCount()
    {
        _skillRefCount++;
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
