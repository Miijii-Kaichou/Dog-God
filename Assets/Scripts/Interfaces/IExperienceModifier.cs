using static SharedData.Constants;

interface IExperienceModifier
{
    public float SetExperienceBonus { get; }
    public BonusModificationType ExperienceModificationType { get; }
    float ExperienceBonus()
    {
        return ExperienceModificationType == BonusModificationType.Whole ?
            SetExperienceBonus : SetExperienceBonus * Hundred;
    }
}
