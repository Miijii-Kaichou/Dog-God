﻿using System;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public sealed class HealthSystem : GameSystem
{
    public static HealthSystem? Self => (HealthSystem?)Instance;

    /*So, the Health System takes in all the things that has
     HP. All of this information will be displayed in game, keeping track
     of everyone's health, that includes the Player's HP, and the Boss's HP.
     
     There will also be a UI portion of the system as well.*/

    public delegate void HealthSystemOperation(string tag);

    public static HealthSystemOperation? onHealthChange;
    public static HealthSystemOperation? onMaxHealthChange;

    public static Dictionary<string, IHealthProperty> Entities = new();

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
}
