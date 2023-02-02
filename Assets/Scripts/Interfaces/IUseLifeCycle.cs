using System;
using UnityEngine;

#nullable enable

interface IUseLifeCycle
{
    // Set the lifetime of anything that needs it in seconds. Like a lasting effect for 20 seconds or a whole minute
    public float LifeDuration { get; }

    // The frequency of which the OnTick is invoked in seconds
    public float TickDuration { get; }

    public Action? OnLifeExpired { get; }
    public Action? OnTick { get; }

    void BeginLifeCycle()
    {
        Alarm alarm = new(2);

        // Keep Track of the time.
        // If time is greater or equal to the LifeDuration,
        // invoke OnLifeExpired method
        alarm.SetFor(LifeDuration, 0, true, () =>
        {
            OnLifeExpired?.Invoke();
        });

        alarm.SetFor(TickDuration, 1, false, () =>
        {
            OnTick?.Invoke();
            if (alarm[0].TimeStarted == false)
                alarm.SetToZero(1, true);
        });
    }
}
