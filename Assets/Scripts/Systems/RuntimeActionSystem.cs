using UnityEngine;

using static SharedData.Constants;

public class RuntimeActionSystem : GameSystem
{
    // The RuntimeActionSystem has reference to 
    // The SkillSystem, WeaponSystem, DeitySystem, and ItemSystem
    // All it does is passes the request made from the KeyboardInputSystem

    private SkillSystem SkillSystem;
    private ItemSystem ItemSystem;
    private MadoSystem MadoSystem;
    private DeitySystem DeitySystem;

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
        AddToSlot(ItemSystemRuntimeID, 1, new ITPurifiedAdulite());
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
        SystemCategories = new IActionCategory[MaxSlots]
        {
            SkillSystem,
            ItemSystem,
            MadoSystem,
            DeitySystem
        };
    }

    public void AddToSlot(int runtimeID, int slotNumber, IActionableItem item)
    {
        this[runtimeID].AddItemToSlot(slotNumber, item);
    }

    public void RemoveFromSlot(int runtimeID, int slotNumber, int count)
    {
        this[runtimeID].RemoveFromSlot(slotNumber, count);
    }
}
