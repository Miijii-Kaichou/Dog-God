using static SharedData.Constants;
// All interfaces used to modify 
// an Item, Mado, Skill, etc.

interface IAttackModifier
{
    // Get Attack Buff or Nerve from this modifier in percentages
    public float SetAttackBonus { get; }
    public BonusModificationType AttackModificationType { get; }
    float AttackBonus
    {
        get
        {
            return AttackModificationType == BonusModificationType.Whole ?
                SetAttackBonus : SetAttackBonus * Hundred;
        }
    }
}
