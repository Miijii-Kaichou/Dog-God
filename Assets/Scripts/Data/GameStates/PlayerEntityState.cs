#nullable enable

using System;
using UnityEngine;

[Serializable]
public sealed class PlayerEntityState
{
    // Don't know what to fill with this, but we'll worry about it later.
    public string? name;
    public int? currentHealth;
    public int? maxHealth;
    public int? currentLevel;
    public int? lives;
    public int? currency;
}
