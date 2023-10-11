#nullable enable

using System;
using System.Collections;
using System.Linq;
using static SharedData.Constants;

public class DeitySystem : GameSystem, IActionCategory
{
    private static DeitySystem? Self;

    public (IActionableItem?[] slots, int[] quantities, int[] capacities, Type?[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize] { One, One, One, One },
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = false
    };

    public readonly static Deity[] DeityList = new Deity[]
    {
        new DAzulette(),
        new DEstere(),
        new DCharolette(),
        new DRosa(),
        new DOthella(),
        new DPatchouli(),
        new DKamui(),
        new DSuisei(),
        new DRyuga(),
    };

    private static DeitySystemState? _SystemState;
    private static int _SkillRefCount;

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<DeitySystem>();
        InitializeDeityStateData();
    }

    private void InitializeDeityStateData()
    {
        _SystemState = new DeitySystemState(DeityList.Length);
    }

    internal static T? GetDeity<T>() where T : Skill
    {
        return (T?)Self?.ActionCategoryDetails.slots.Where(skill => skill?.StaticItemType == typeof(T)).Single();
    }

    internal static int GetRefCount()
    {
        return _SkillRefCount;
    }

    internal static void IncreaseRefCount()
    {
        _SkillRefCount++;
    }

    internal static void GainAccess(int index)
    {
        _SystemState.madeContract[index] = true;
    }

    internal static void Lock(int index)
    {
        _SystemState.madeContract[index] = false;
    }

    internal static Deity? LocateDeity<T>()
    {
        return DeityList.Where(deity => deity.StaticItemType == typeof(T)).Single();
    }

    internal static void Save()
    {
        PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].UpdateDeityStateData(_SystemState);
    }
}
