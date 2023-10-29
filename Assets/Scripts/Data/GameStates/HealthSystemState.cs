#nullable enable

using System;
using System.Collections.Generic;

[Serializable]
public sealed class HealthSystemState
{
    public HealthSystemState(Dictionary<string, IHealthProperty> data)
    {
        Dictionary<string, (float health, float maxHealth)> convertedTable = new Dictionary<string, (float, float)>();

        foreach (var set in data)
        { 
            convertedTable.Add(set.Key, (set.Value.HealthValue, set.Value.MaxHealthValue));
        }

        Data = convertedTable;
    }

    public Dictionary<string, (float health, float maxHealth)> Data { get; }
}
