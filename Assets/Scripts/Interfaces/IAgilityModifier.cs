using static SharedData.Constants;

interface IAgilityModifier
{
    // Buff or nerf Agility stat by a set percentage;
    public float SetAgilityBonus { get; }
    public BonusModificationType AgilityModificationType { get; }
    float AgilityBonus
    {
        get
        {
            return AgilityModificationType == BonusModificationType.Whole ?
                SetAgilityBonus : SetAgilityBonus * Hundred;
        }
    }
}
