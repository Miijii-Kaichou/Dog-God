#nullable enable

using UnityEngine;

public class ManaSystem : GameSystem, IRegisterEntity<IManaProperty>
{
    /*So, the Mana System takes in all the things that has
    HP. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's MP.

    There will also be a UI portion of the system as well.*/
    public delegate void ManaSystemOperation();

    public ManaSystemOperation? onManaChange;

    public IManaProperty? EntityReference { get; set; }


    internal void SetMana(float value, bool isRelative = false)
    {
        if (isRelative)
            EntityReference?.AddToMana(value);
        if (!isRelative)
            EntityReference?.SetMana(value);

        if (EntityReference!.ManaValue > EntityReference!.MaxManaValue)
        {
            EntityReference.SetMana(EntityReference!.MaxManaValue);
        }
        onManaChange?.Invoke();
    }

    internal void SetMaxMana(float value)
    {
        EntityReference?.SetMaxMana(value);
    }

    internal void RestoreAllMana()
    {
        SetMaxMana(EntityReference!.MaxManaValue);
    }
}
