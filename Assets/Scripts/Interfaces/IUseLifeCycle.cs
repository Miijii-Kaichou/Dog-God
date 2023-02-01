interface IUseLifeCycle
{
    // Set the lifetime of anything that needs it in seconds. Like a lasting effect for 20 seconds or a whole minute
    public float LifeDuration { get; }

    // The frequency of which the OnTick is invoked in seconds
    public float TickDuration { get; }

    void BeginLifeCycle()
    {
        // Keep Track of the time.
        // If time is greater or equal to the LifeDuration,
        // invoke OnLifeExpired method
    }

    void OnLifeExpired() { }

    void OnTick() { }
}
