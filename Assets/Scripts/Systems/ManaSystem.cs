using UnityEngine.UI;

public class ManaSystem : GameSystem, IRegisterPlayer<IManaProperty>
{
    /*So, the Mana System takes in all the things that has
    HP. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's MP.

    There will also be a UI portion of the system as well.*/
    public delegate void ManaSystemOperation();

    public ManaSystemOperation? onManaChange;

    public Slider playerManaSlider;
    public float playerMaxMana;

    public IManaProperty EntityRef { get; set; }

    protected override void OnInit()
    {
        //We give the max value for the boss, and the player.
        if (EntityRef != null)
        {
            EntityRef.ManaValue = playerMaxMana;
        }
    }

    protected override void Main()
    {
        if (playerManaSlider != null)
            ManagerHealthMeter();
    }

    /// <summary>
    /// Manages both the player's health, and the boss' health
    /// </summary>
    void ManagerHealthMeter()
    {
        playerManaSlider.value = EntityRef.ManaValue / playerMaxMana;
    }

    internal void SetMana(float value, bool isRelative)
    {
        if (isRelative)
            EntityRef.AddToMana(value);
        if(!isRelative)
            EntityRef.SetMana(value);
        onManaChange?.Invoke();
    }

    internal void SetMaxMana(float value)
    {
        EntityRef.SetMaxMana(value);
        
    }
}
