public interface IHealthProperty
{
    public float HealthValue { get; set; }
    public float MaxHealthValue { get; set; }
    const float DefaultMaxHealth = 100;

    public void AddHealth(float value)
    {
        HealthValue += value;
    }

    public void SetHealth(float value)
    {
        HealthValue = value;
    }

    public void SetMaxHealth(float value)
    {
        MaxHealthValue = value;
    }
}
