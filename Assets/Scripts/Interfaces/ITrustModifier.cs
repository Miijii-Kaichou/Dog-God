interface ITrustModifier
{
    // Buff or nerf Trust stats by a set percentage
    public float TrustBonus { get; }
    public BonusModificationType TrustModificationType { get; }
}
