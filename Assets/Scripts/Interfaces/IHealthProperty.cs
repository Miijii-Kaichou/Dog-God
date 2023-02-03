#nullable enable

using Extensions;
using System;
using UnityEngine;

public interface IHealthProperty
{
    public float HealthValue { get; set; }
    public float MaxHealthValue { get; set; }
    public Action? OnHealthDown {get;set;}
    public Action? OnHealthNegativeChange { get; set; }
    public Action? OnHealthPositiveChange { get; set; }

    const float DefaultMaxHealth = 100;

    public void AddHealth(float value)
    {
        HealthValue += value;

        if (value.ToSign() == Sign.Negative)
            OnHealthNegativeChange?.Invoke();
        if (value.ToSign() == Sign.Positive)
            OnHealthPositiveChange?.Invoke();
        if (HealthValue <= 0)
            OnHealthDown?.Invoke();
    }

    public void SetHealth(float value)
    {
        HealthValue = value;
        if (HealthValue <= 0) OnHealthDown?.Invoke();
    }

    public void SetMaxHealth(float value)
    {
        MaxHealthValue = value;
    }

    void RestoreAllHealth()
    {
        HealthValue = MaxHealthValue;
    }
}
