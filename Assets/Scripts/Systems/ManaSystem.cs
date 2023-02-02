#nullable enable

using UnityEngine;

public sealed class ManaSystem : GameSystem, IRegisterEntity<IManaProperty>
{
    /*So, the Mana System takes in all the things that has
    HP. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's MP.

    There will also be a UI portion of the system as well.*/
    public delegate void ManaSystemOperation();

    public ManaSystemOperation? onManaChange;

    public IManaProperty? _player { get; set; }


    internal void SetMana(float value, bool isRelative = false)
    {
        if (isRelative)
            _player?.AddToMana(value);
        if (!isRelative)
            _player?.SetMana(value);

        if (_player!.ManaValue > _player!.MaxManaValue)
        {
            _player.SetMana(_player!.MaxManaValue);
        }
        onManaChange?.Invoke();
    }

    internal void SetMaxMana(float value)
    {
        _player?.SetMaxMana(value);
    }

    internal void RestoreAllMana()
    {
        SetMaxMana(_player!.MaxManaValue);
    }
}
