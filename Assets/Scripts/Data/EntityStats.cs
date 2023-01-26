using System.Collections.Generic;
using UnityEngine;

public enum StatVariable
{
    STRENGTH,
    VITALITY,
    DEXTERITY,
    ENDURANCE,
    CRITCAL,
    FAITH,
    INTELLIGENCE,
    LUCK
}

public enum DamageStatVariable
{
    NONE = 0 << 0,
    PHYSICAL = 0 << 1,
    BURNING = 0 << 2,
    FREEZING = 0 << 3,
    POISON = 0 << 4,
    LIGHTNING = 0 << 5,
    LIGHTSPIRITUAL = 0 << 6,
    DARKSPIRITUAL = 0 << 7,
    ONEHAND = 0 << 8,
    TWOHAND = 0 << 9,
    DUALWIELD = 0 << 10
}


[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class EntityStats : ScriptableObject
{
    /*Mainly for the Player, they are given these kinds of stats:
        Strength
        Vitality
        Dexterity
        Endurance
        Critical
        Faith
        Intelligence
        Luck

        For Weapons, it varies, such as how much damage they have
        the require status to wield it one handed, two handed, or dual-wield it,
        consider it considers that option for you,
         */

    //The reason that we have StatProperty is simple because stats can vary depending on the object.
    //Stat Property can either be damage, health, mana, all player stats, etc.
    //Having it this way will allow us to access the property by name, and retreive it.
    public List<StatProperty> properties;
}
