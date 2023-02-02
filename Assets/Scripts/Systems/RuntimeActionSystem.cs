using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static SharedData.Constants;

public sealed class RuntimeActionSystem : GameSystem
{
    // The RuntimeActionSystem has reference to 
    // The SkillSystem, WeaponSystem, DeitySystem, and ItemSystem
    // All it does is passes the request made from the KeyboardInputSystem

    public static SkillSystem SkillSystem { get; private set; }
    public static ItemSystem ItemSystem { get; private set; }
    public static MadoSystem MadoSystem { get; private set; }
    public static DeitySystem DeitySystem { get; private set; }

    Dictionary<Type, IActionCategory> SystemCategories { get; set; }

    private const int MaxSlots = 4;

    public IActionCategory this[Type type]
    {
        get {
            if (SystemCategories == null) InitalizeCategories();
            return SystemCategories[type];
        }
    }

    public IActionCategory this[int index]
    {
        get
        {
            if (SystemCategories == null) InitalizeCategories();
            Type targetType = index switch
            {
                0 => typeof(SkillSystem),
                1 => typeof(ItemSystem),
                2 => typeof(MadoSystem),
                3 => typeof(DeitySystem),
                _ => throw new NotImplementedException(),
            };
            return this[targetType];
        }
    }

    protected override void OnInit()
    {
        GetSystems();
        AddToSlot<ItemSystem>( 1, new ITPurifiedAdulite());
    }

    void GetSystems()
    {
        Debug.Log("Getting systems...");
        SkillSystem = GameManager.GetSystem<SkillSystem>();
        MadoSystem = GameManager.GetSystem<MadoSystem>();
        DeitySystem = GameManager.GetSystem<DeitySystem>();
        ItemSystem = GameManager.GetSystem<ItemSystem>();
    }

    void InitalizeCategories()
    {
        SystemCategories = new Dictionary<Type, IActionCategory>(MaxSlots)
        {
            { typeof(SkillSystem),  SkillSystem  },
            { typeof(ItemSystem),   ItemSystem   },
            { typeof(MadoSystem),   MadoSystem   },
            { typeof(DeitySystem),   DeitySystem }
        };
    }

    public void AddToSlot<T>(int slotNumber, IActionableItem item) where T : GameSystem
    {
        this[typeof(T)].AddItemToSlot(slotNumber, item);
    }

    public void RemoveFromSlot<T>( int slotNumber, int count)
    {
        this[typeof(T)].RemoveFromSlot(slotNumber, count);
    }
}
