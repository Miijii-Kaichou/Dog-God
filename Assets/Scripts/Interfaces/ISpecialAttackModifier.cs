interface ISpecialAttackModifier
{
    // Buff or nerf Special Attack (Skills and Divine Skills) by a set percentage
    public float SpecialAttackBonus { get; }
    public BonusModificationType SpecialAttackModificationType { get; }
}
