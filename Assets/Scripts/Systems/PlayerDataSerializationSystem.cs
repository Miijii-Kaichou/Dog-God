using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public sealed class PlayerDataSerializationSystem : GameSystem
{
    public static Action OnLoadDone { get; internal set; }

    public static PlayerDataState[] PlayerDataStateSet { get; private set; } =
    {
        new PlayerDataState(),
        new PlayerDataState(),
        new PlayerDataState()
    };

    static string _DataStateDirectory = Application.persistentDataPath + @"/Data";

    internal static void LoadPlayerDataState(int profileIndex,out PlayerDataState deserializedState)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(_DataStateDirectory);
        stringBuilder.AppendLine(@"/state");
        stringBuilder.AppendLine(profileIndex.ToString());
        stringBuilder.AppendLine(".dat");
        using (FileStream stream = new(stringBuilder.ToString(), FileMode.Open))
        {
            BinaryFormatter formatter = new();
            PlayerDataStateSet[profileIndex] = formatter.Deserialize(new BufferedStream(stream)) as PlayerDataState;
        }

        deserializedState = PlayerDataStateSet[profileIndex];
    }

    internal static void SavePlayerDataState(int profileIndex)
    {
        if (!File.Exists(_DataStateDirectory))
        {
            Directory.CreateDirectory(_DataStateDirectory);
        }
        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(_DataStateDirectory);
            stringBuilder.AppendLine(@"/state");
            stringBuilder.AppendLine(profileIndex.ToString());
            stringBuilder.AppendLine(".dat");
            using (FileStream stream = new FileStream(stringBuilder.ToString(), FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new();
                formatter.Serialize(new BufferedStream(stream), PlayerDataStateSet[profileIndex]);
            }
        } 
        catch
        {
            Debug.LogError($"Failed to save to state{profileIndex}.dat");
        }

        Debug.Log($"state{profileIndex}.dat Sucessfully Saved");
    }
}

public sealed class PlayerDataState
{
    public int DataID { get; private set; } = 0;

    PlayerEntity _playerEntity;
    StatsSystem _statSystem;
    ItemSystem _itemSystem;
    SkillSystem _skillSystem;
    MadoSystem _madoSystem;
    DeitySystem _deitySystem;

    public PlayerDataState()
    {
        DataID = PlayerDataSerializationSystem.PlayerDataStateSet.Length;
    }

    public void UpdatePlayerEntityStateData(PlayerEntity instance) => _playerEntity = instance;
    public void UpdateStatStateData(StatsSystem instance) => _statSystem = instance;
    public void UpdateItemStateData(ItemSystem instance) => _itemSystem = instance;
    public void UpdateSkillStateData(SkillSystem instance) => _skillSystem = instance;
    public void UpdateMadoStateData(MadoSystem instance) => _madoSystem = instance;
    public void UpdateDeityStateData(DeitySystem instance) => _deitySystem = instance;

    public PlayerEntity GetPlayerEntityStateData() => _playerEntity;
    public StatsSystem GetStatsSystemStateData() => _statSystem;
    public ItemSystem GetItemSystemStateData() => _itemSystem;
    public SkillSystem GetSkillSystemStateData() => _skillSystem;
    public MadoSystem GetMadoSystemStateData() => _madoSystem;
    public DeitySystem GetDeitySystemStateData() => _deitySystem;
}