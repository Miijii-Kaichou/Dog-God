interface IPoiseModifier
{
    // Buff or nerf Poise stat by a set percentage;
    public float PoiseBonus { get; }
    public BonusModificationType PoiseModificationType { get; }
}
