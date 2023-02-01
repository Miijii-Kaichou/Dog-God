using static SharedData.Constants;

interface IManaModifier
{
    // Increase Mana, Decrease Mana, or Enhance Max Mana on Command
    public float SetManaBonus { get; }
    public BonusModificationType ManaModificationType { get; }
    float ManaBonus
    {
        get
        {
            return ManaModificationType == BonusModificationType.Whole ?
                SetManaBonus : SetManaBonus * Hundred;
        }
    }
}
