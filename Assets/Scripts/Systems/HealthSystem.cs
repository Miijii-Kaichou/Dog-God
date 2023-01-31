using System;
using System.Collections.Generic;

#nullable enable
public class HealthSystem : GameSystem
{
    /*So, the Health System takes in all the things that has
     HP. All of this information will be displayed in game, keeping track
     of everyone's health, that includes the Player's HP, and the Boss's HP.
     
     There will also be a UI portion of the system as well.*/

    public delegate void HealthSystemOperation(string tag);

    public HealthSystemOperation? onHealthChange;
    public HealthSystemOperation? onMaxHealthChange;

    public Dictionary<string, IHealthProperty> entities = new();

    public IHealthProperty this[string id]
    {
        get { return entities[id]; }
    }

    public void AddNewEntry(string id, IHealthProperty entity)
    {
        entities.Add(id, entity);
    }

    internal void SetHealth(string id, float value, bool isRelative = false)
    {
        if (isRelative)
            entities[id].AddHealth(value);
        if (!isRelative)
            entities[id].SetHealth(value);
        onHealthChange?.Invoke(id);
    }
     
    internal void SetMaxHealth(string id, float value)
    {
        entities[id].SetMaxHealth(value);
        onMaxHealthChange?.Invoke(id);
    }

    internal bool Exists(string playerEntityTag)
    {
        return entities.ContainsKey(playerEntityTag);
    }
}
