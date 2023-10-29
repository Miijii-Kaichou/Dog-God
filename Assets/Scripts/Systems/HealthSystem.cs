#nullable enable

using System.Collections.Generic;

public sealed class HealthSystem : GameSystem
{
    private static HealthSystemState? _SystemState;

    public static HealthSystem? Self;

    /*So, the Health System takes in all the things that has
     HP. All of this information will be displayed in game, keeping track
     of everyone's health, that includes the Player's HP, and the Boss's HP.
     
     There will also be a UI portion of the system as well.*/

    public delegate void HealthSystemOperation(string tag);

    public static HealthSystemOperation? onHealthChange;
    public static HealthSystemOperation? onMaxHealthChange;

    public static Dictionary<string, IHealthProperty> Entities = new();

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<HealthSystem>();
    }

    public IHealthProperty this[string id]
    {
        get { return Entities[id]; }
    }

    public static void AddNewEntry(string id, IHealthProperty entity)
    {
        Entities.Add(id, entity);
    }

    internal static void SetHealth(string id, float value, bool isRelative = false)
    {
        if (isRelative)
            Entities[id].AddHealth(value);
        if (!isRelative)
            Entities[id].SetHealth(value);

        if (Entities[id].HealthValue > Entities[id].MaxHealthValue)
        {
            Entities[id].SetHealth(Entities[id].MaxHealthValue);
        }

        onHealthChange?.Invoke(id);
    }
     
    internal static void SetMaxHealth(string id, float value)
    {
        Entities[id].SetMaxHealth(value);
        onMaxHealthChange?.Invoke(id);
    }

    internal static bool Exists(string playerEntityTag)
    {
        return Entities.ContainsKey(playerEntityTag);
    }

    internal static void RestoreAllHealth(string id)
    {
        Entities[id].RestoreAllHealth();
    }

    public static void Save()
    {
        var data = Entities;
        _SystemState = new(data);
        PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].UpdateHealthStateData(_SystemState);
    }

    public static void Load()
    {
        _SystemState = PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].GetHealthStateData();

        var data = _SystemState.Data;

        foreach (var set in data)
        {
            float health = set.Value.health;
            float maxHealth = set.Value.maxHealth;

            Entities[set.Key].SetHealth(health);
            Entities[set.Key].SetMaxHealth(maxHealth);
        }
    }
}