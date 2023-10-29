#nullable enable

using System;
using System.Linq;
using Extensions;
using static SharedData.Constants;

public sealed class ItemSystem : GameSystem, IActionCategory
{
    private static ItemSystemState? _SystemState;
    private static ItemSystem? Self;

    public (IActionableItem?[] slots, int[] quantities, int[] capacities, Type?[] requiredTypes, bool isExpensible) ActionCategoryDetails { get; set; } = new()
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

    public readonly static Item[] ItemList = {
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


    private static int _ItemRefCount;

    protected override void OnInit()
    {
        _SystemState = new ItemSystemState(ItemList.Length);
        SetUpCatalog(_SystemState);
        //TODO: Read Player Data, and figure out what's locked and
        // what's unlocked.
    }

    private void SetUpCatalog(ItemSystemState systemState)
    {
        // Create shop catalog stuff here.
        for (int index = 0; index < ItemList.Length; index++)
        {
            var item = ItemList[index];
            systemState.shopEntries[index].New(item.ItemName, item.ShopValue, item.ShopImage);
        } 
    }

    protected override void Main()
    {
        Self ??= GameManager.GetSystem<ItemSystem>();
    }

    internal static T? GetItem<T>() where T : Item
    {
        return (T?)Self!.ActionCategoryDetails.slots.Where(item => item?.StaticItemType == typeof(T)).Single();
    }

    internal static void IncreaseRefCount()
    {
        _ItemRefCount++;
    }

    internal static int GetRefCount()
    {
        return _ItemRefCount;
    }

    // We'll have to change these methods to 
    // allocate an amount to the player and so on.
    //internal static void GainAccess(int id)
    //{
    //    _SystemState!.[id] = true;
    //}

    //internal static void Lock(int id)
    //{
    //    _SystemState![id] = false;
    //}

    internal static void EnhanceEffectivenessForAllItems(int magnitude)
    {
        throw new NotImplementedException();
    }

    internal static void ReduceEffectivenessForAllItems(int two)
    {
        throw new NotImplementedException();
    }

    internal static void Load()
    {
       _SystemState = PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].GetItemStateData();
    }

    internal static void Save()
    {
        PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].UpdateItemStateData(_SystemState);
    }
}