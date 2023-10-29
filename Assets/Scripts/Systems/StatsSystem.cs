#nullable enable

using UnityEngine;
using System.Text;
using System;

[Serializable]
public sealed class StatsSystemState
{
    public StatsSystemState(EntityStats stats)
    {
        Stats = stats;
    }

    public EntityStats Stats { get; }
}

public sealed class StatsSystem : GameSystem
{
    private static StatsSystem? Self;
    static StatsSystemState? _SystemState;
    static int ActiveProfile => GameManager.ActiveProfileIndex;

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<StatsSystem>();
    }

    public static void Save()
    {
        PlayerDataSerializationSystem.PlayerDataStateSet[ActiveProfile].UpdateStatStateData(_SystemState);
    }

    public static void Load()
    {
        _SystemState = PlayerDataSerializationSystem.PlayerDataStateSet[ActiveProfile].GetStatsStateData();
    }

    public static void SetPlayerStatsState(EntityStats newState)
    {
        _SystemState = new StatsSystemState(newState); 
    }

    public static StatsSystemState? GetPlayerStatsState()
    {
        return _SystemState;
    }

    private static void DoPrint()
    {
        StringBuilder statReport = new();
        for(int i = 0; i < _SystemState!.Stats!.Size; i++)
        {
            var stat = _SystemState.Stats[(StatVariable)i];
            statReport.Append($"Stat {(StatVariable)i} => {stat}");
            if (i >= _SystemState.Stats.Size - 1) continue;
            statReport.Append('\n');
        }
        Debug.Log(statReport.ToString());
    }
}