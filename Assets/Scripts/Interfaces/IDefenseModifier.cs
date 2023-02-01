using static SharedData.Constants;

interface IDefenseModifier
{
    // Get Defense Buff or Nerf from this modifier in percentage
    public float SetDefenseBonus { get; }
    public BonusModificationType DefenseModificationType { get; }
    float DefenseBonus
    {
        get
        {
            return DefenseModificationType == BonusModificationType.Whole ?
                SetDefenseBonus : SetDefenseBonus * Hundred;
        }
    }
}
