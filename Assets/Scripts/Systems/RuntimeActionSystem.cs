using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cakewalk.IoC;

public class RuntimeActionSystem : GameSystem
{
    // The RuntimeActionSystem has reference to 
    // The SkillSystem, WeaponSystem, DeitySystem, and ItemSystem
    // All it does is passes the request made from the KeyboardInputSystem

    private SkillSystem SkillSystem;
    private WeaponSystem WeaponSystem;
    private DeitySystem DeitySystem;
    private ItemSystem ItemSystem;

    IActionCategory[] SystemCategories { get; set; }

    private const int MaxSlots = 4;

    public IActionCategory this[int categoryIndex]
    {
        get { return SystemCategories[categoryIndex]; }
    }

    protected override void OnInit()
    {
        SkillSystem = GameManager.Command.GetSystem<SkillSystem>();
        WeaponSystem = GameManager.Command.GetSystem<WeaponSystem>();
        DeitySystem = GameManager.Command.GetSystem<DeitySystem>();
        ItemSystem = GameManager.Command.GetSystem<ItemSystem>();

        InitalizeCategories();
    }

    void InitalizeCategories()
    {
        SystemCategories = new IActionCategory[MaxSlots]
        {
            SkillSystem,
            ItemSystem,
            WeaponSystem,
            DeitySystem
        };
    }
}
