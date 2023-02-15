#nullable enable

using System;
using System.IO;
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

    static string _DataStateDirectory;

    private void Awake()
    {
        _DataStateDirectory = Application.persistentDataPath + @"/Profiles";
    }

    internal static void CheckIfStateFileExists(int profileIndex, out StringBuilder? result)
    {
        var path = GenerateProfilePath(profileIndex);
        result = File.Exists(path.ToString()) ? path : null;
    }

    internal static void LoadPlayerDataState(int profileIndex, out PlayerDataState? deserializedState)
    {
        deserializedState = null;
        CheckIfStateFileExists(profileIndex, out StringBuilder? stringBuilder);

        if (stringBuilder == null) return;

        using (FileStream stream = new(stringBuilder!.ToString(), FileMode.Open))
        {
            BinaryFormatter formatter = new();
            PlayerDataStateSet[profileIndex] = (formatter!.Deserialize(new BufferedStream(stream)) as PlayerDataState)!;
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
            StringBuilder stringBuilder = GenerateProfilePath(profileIndex);
            using FileStream stream = new(stringBuilder.ToString(), FileMode.OpenOrCreate);
            BinaryFormatter formatter = new();
            formatter.Serialize(new BufferedStream(stream), PlayerDataStateSet[profileIndex]);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save to state{profileIndex}.dat\n " +
                $"Reason: {e.Message}");
            return;
        }

        Debug.Log($"state{profileIndex}.dat Sucessfully Saved");
    }

    private static StringBuilder GenerateProfilePath(int profileIndex)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_DataStateDirectory);
        stringBuilder.Append(@"/state");
        stringBuilder.Append(profileIndex.ToString());
        stringBuilder.Append(".dat");
        return stringBuilder;
    }
}
