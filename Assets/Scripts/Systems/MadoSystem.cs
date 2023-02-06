#nullable enable

using System;
using System.Collections;
using System.Linq;

using static SharedData.Constants;

public sealed class MadoSystem : GameSystem, IActionCategory
{
    private static MadoSystem? Self;

    public (IActionableItem?[] slots, int[] quantities, int[] capacities, Type?[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize]{One, One, One, One},
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    private readonly static Mado[] MadoList = new Mado[]
    {
        new MDPyromado(),
        new MDCryomado(),
        new MDHyromado(), 
        new MDYamimado(), 
        new MDNichimado(),
        new MDTsukimado(),
        new MDYotsumado(), 
    };

    private static BitArray? Accessibility;
    private static int _MadoRefCount;

    static IActionCategory? Category => Self;

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<MadoSystem>();
        Accessibility = new BitArray(MadoList.Length);

        //ToDO: Read Palyer data, and figure out what's unlocked
    }

    protected override void Main()
    {
        base.Main();

    }

    internal static void EquipMadoToSlot(int index, int slotNumber)
    {
        if (Accessibility![index] == false) return;
        Category!.AddItemToSlot(slotNumber, MadoList[index]);
    }

    internal static void UnequipMadoFromSlot(int slotNumber)
    {
        Category!.RemoveFromSlot(slotNumber);
    }

    internal static T? GetMado<T>() where T : Mado
    {
        return (T?)Self!.ActionCategoryDetails.slots.Where(item => item?.StaticItemType == typeof(T)).Single();
    }

    internal static void IncreaseRefCount()
    {
        _MadoRefCount++;
    }

    internal static int GetRefCount()
    {
        return _MadoRefCount;
    }

    internal static void GainAccess(int index)
    {
        Accessibility![index] = true;
    }

    internal static void Lock(int index)
    {
        Accessibility![index] = false;
    }

    internal static void FindTraitsOf(MDPyromado madoEnhancer)
    {
        throw new NotImplementedException();
    }
}
