interface ISpecialDefenceModifier
{
    // Buff or nerf Special Defense (Parries) by a set percentage
    public float SpecialDefenseBonus { get; }
    public BonusModificationType SpecialDefenseModificationType { get; }
}
