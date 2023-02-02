using System;
using System.Collections;
using System.Linq;
using static SharedData.Constants;

public sealed class ItemSystem : GameSystem, IActionCategory
{
    public (IActionableItem[] slots, int[] quantities, int[] capacities, Type[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
    {
        slots = new IActionableItem[MaxSlotSize],
        quantities = new int[MaxSlotSize],
        capacities = new int[MaxSlotSize]
        {
            MaxQuantity,
            MaxQuantity,
            MaxQuantity,
            MaxQuantity
        },
        requiredTypes = new Type[MaxSlotSize],
        isExpensible = true
    };
    public Type[] RequiredTypes { get; }

    public Item[] ItemList = {
        new ITAdulite(),
        new ITAlguarde(),
        new ITAlhercule(),
        new ITChargedAdulite(),
        new ITEther(),
        new ITEtherAlpha(),
        new ITEtherOmega(),
        new ITMagusCrystal(),
        new ITMagusPotion(),
        new ITMagusPotionAlpha(),
        new ITMagusPotionDelta(),
        new ITMagusPotionOmega(),
        new ITMagusShard(),
        new ITPotion(),
        new ITPotionAlpha(),
        new ITPotionDelta(),
        new ITPotionOmega(),
        new ITPotionGrande(),
        new ITPurifiedAdulite(),
        new ITStellaLeaf()
    };

    private BitArray Accessibility;

    private int _itemRefCount;

    protected override void OnInit()
    {
        Accessibility = new BitArray(ItemList.Length);

        //TODO: Read Player Data, and figure out what's locked and
        // what's unlocked.
    }

    internal T GetItem<T>() where T : Item
    {
        return (T)ActionCategoryDetails.slots.Where(item => item.StaticItemType == typeof(T)).Single();
    }

    internal void IncreaseRefCount()
    {
        _itemRefCount++;
    }

    internal int GetRefCount()
    {
        return _itemRefCount;
    }

    internal void GainAccess(int id)
    {
        Accessibility[id] = true;
    }

    internal void Lock(int id)
    {
        Accessibility[id] = false;
    }
}