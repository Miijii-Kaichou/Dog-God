#nullable enable

using UnityEngine;

public sealed class ManaSystem : GameSystem
{
    private static ManaSystem? Self => (ManaSystem?)Instance;

    /*So, the Mana System takes in all the things that has
    HP. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's MP.

    There will also be a UI portion of the system as well.*/
    public delegate void ManaSystemOperation();

    public static ManaSystemOperation? OnManaChange;
    private static IManaProperty PlayerManaProp => Self!.Player;

    internal static void SetMana(float value, bool isRelative = false)
    {
        if (isRelative)
            PlayerManaProp.AddToMana(value);
        if (!isRelative)
            PlayerManaProp.SetMana(value);

        if (PlayerManaProp.ManaValue > PlayerManaProp.MaxManaValue)
        {
            PlayerManaProp.SetMana(PlayerManaProp.MaxManaValue);
        }
        OnManaChange?.Invoke();
    }

    internal static void SetMaxMana(float value)
    {
        PlayerManaProp.SetMaxMana(value);
    }

    internal static void RestoreAllMana()
    {
        SetMaxMana(PlayerManaProp.MaxManaValue);
    }
}
