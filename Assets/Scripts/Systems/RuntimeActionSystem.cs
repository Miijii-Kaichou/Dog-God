using UnityEngine;

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
        get {
            if (SystemCategories == null) InitalizeCategories();
            return SystemCategories[categoryIndex];
        }
    }

    protected override void OnInit()
    {
        GetSystems();
    }

    void GetSystems()
    {
        Debug.Log("Getting systems...");
        SkillSystem = GameManager.GetSystem<SkillSystem>();
        WeaponSystem = GameManager.GetSystem<WeaponSystem>();
        DeitySystem = GameManager.GetSystem<DeitySystem>();
        ItemSystem = GameManager.GetSystem<ItemSystem>();
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
