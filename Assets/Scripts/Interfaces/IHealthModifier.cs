using static SharedData.Constants;

public interface IHealthModifier
{
    // Increase Health, Decrease Health, or Enhance Max Health by a set percentage or value
    public float SetHealthBonus { get; }
    public BonusModificationType HealthModificationType { get; }
    float HealthBonus
    {

        get
        {
            return HealthModificationType == BonusModificationType.Whole ?
                SetHealthBonus : SetHealthBonus * Hundred;
        }
    }
}
