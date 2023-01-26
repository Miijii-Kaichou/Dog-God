public interface IManaProperty
{
    public float ManaValue { get; set; }
    public float MaxManaValue{ get; set; }

    public void AddToMana(float value)
    {
        ManaValue += value;
    }

    public void RemoveMana(float value)
    {
        ManaValue -= value;
    }

    public void SetMana(float value)
    {
        ManaValue = value;
    }

    public void SetMaxMana(float value)
    {
        MaxManaValue = value;
    }
}
