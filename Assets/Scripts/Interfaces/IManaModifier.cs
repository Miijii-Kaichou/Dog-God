using static SharedData.Constants;

#nullable enable

interface IManaModifier
{
    // Increase Mana, Decrease Mana, or Enhance Max Mana on Command
    public float SetManaBonus { get; }
    public BonusModificationType ManaModificationType { get; }
    public ManaSystem? ManaSystem { get; set; }
    float ManaBonus
    {
        get
        {
            return ManaModificationType == BonusModificationType.Whole ?
                SetManaBonus : SetManaBonus * Hundred;
        }
    }
}
