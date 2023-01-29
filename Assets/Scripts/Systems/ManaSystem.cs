using UnityEngine.UI;

#nullable enable

public class ManaSystem : GameSystem, IRegisterPlayer<IManaProperty>
{
    /*So, the Mana System takes in all the things that has
    HP. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's MP.

    There will also be a UI portion of the system as well.*/
    public delegate void ManaSystemOperation();

    public ManaSystemOperation? onManaChange;

    public IManaProperty? EntityRef { get; set; }


    internal void SetMana(float value, bool isRelative = false)
    {
        if (isRelative)
            EntityRef?.AddToMana(value);
        if (!isRelative)
            EntityRef?.SetMana(value);
        onManaChange?.Invoke();
    }

    internal void SetMaxMana(float value)
    {
        EntityRef?.SetMaxMana(value);
    }
}
