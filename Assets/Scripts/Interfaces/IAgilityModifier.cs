interface IAgilityModifier
{
    // Buff or nerf Agility stat by a set percentage;
    public float AgilityBonus { get; }
    public BonusModificationType AgilityModificationType { get; }
}
