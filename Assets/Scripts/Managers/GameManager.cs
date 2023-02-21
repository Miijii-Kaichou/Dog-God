#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

using static SharedData.Constants;

public sealed class GameManager : Singleton<GameManager>
{
    [SerializeField, Header("Sprite Atlas")]
    SpriteAtlas? spriteAtlas;

    [SerializeField, Header("System Dialogue File")]
    XVNMLAsset mainXVNML;

    public static SpriteAtlas? SpriteAtlas
    {
        get { return Instance?.spriteAtlas; }
    }

    public static UniversalGameState GameState;

    public static Action? OnSystemRegistrationProcessCompleted { get; internal set; }
    public static Action? OnSystemStartProcessCompleted { get; internal set; }
    public static Action? OnPlayerRegistered { get; internal set; }
    public static Action? OnBossRegistered { get; internal set; }

    public static T? GetSystem<T>() where T : GameSystem => Instance?.GetGameSystem<T>();
    public static SystemStatus? GetSystemStatus(GameSystem system) => Instance?.GetSystemStatus(system.SystemName);
    public static SystemInfo[]? GetSystemInfo() => Instance?.GetAllSystemInfo();
    public static SystemInfo[]? GetSystemInfo(Status _status) => Instance?.GetAllSystemInfo(_status);

    public static int ActiveProfileIndex { get; private set; } = -1;

    public struct Achievement
    {
        string title;

        DateTime achievementReceivedDate;

        public enum Rarity
        {
            COMMON,
            RARE,
            SUPER_RARE
        }
        Rarity rarity;

        bool isAchieved;

        public string GetTitle() => title;
        public Rarity GetAchievementRarity() => rarity;

        public bool GetAchievementStatus() => isAchieved;
        public DateTime GetReceivalDate() => achievementReceivedDate;
        public double GetDaysSinceAchievement() => GetReceivalDate().TimeOfDay.TotalDays;
    }

    public enum SortingOrder
    {
        TITLE,
        DATE,
        DAYS,
        MONTH,
        HOURS,
        MINUTES,
        SECONDS,
        RARITY
    }

    /*GameManager will have a whole collect of different systems and their statuses.*/

    [Header("Resolution")]
    public uint resolutionWidth = 1920;
    public uint resolutionHeight = 1660;

    [Header("Game Systems"), ShowAsSystemIndicator]
    public List<SystemInfo> systemInfoList = new List<SystemInfo>();

    DirectoryInfo? dirInfo;

    [Header("Achievements"), SerializeField]
    private List<Achievement> achievements = new List<Achievement>();
    internal static PlayerDataState? PlayerDataState;
    internal static string? PlayerName;

    public static PlayerEntity? Player { get; set; }
    public static BossEntity? Boss { get; set; }

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion  
        RegisterSystems();
        StartUpAllSystems();
        ReportStartUp();
    }

    public static void ReferencePlayer(PlayerEntity player)
    {
        Player = player;
        OnPlayerRegistered?.Invoke();
    }

    public static void ReferenceBoss(BossEntity boss)
    {
        Boss = boss;
        OnPlayerRegistered?.Invoke();
    }

    public static void UpdateActivePlayerID(int id)
    {
        ActiveProfileIndex = id;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution((int)resolutionWidth, (int)resolutionHeight, FullScreenMode.FullScreenWindow, Application.targetFrameRate);

        //I want to also create a folder of Profiles if one exists or not.

        dirInfo = new DirectoryInfo(Application.persistentDataPath + "/Profiles");

        if (!dirInfo.Exists)
            CreateProfileDirectory();
    }

    void RegisterSystems()
    {
        GameSystem[] allGameSystems = FindObjectsOfType<GameSystem>();
        foreach (GameSystem entry in allGameSystems)
        {
            SystemInfo newSystemInfo = new SystemInfo
            {
                systemName = entry.SystemName,
                system = entry,
                systemStatus = new SystemStatus()
            };

            systemInfoList.Add(newSystemInfo);
        }
        systemInfoList = systemInfoList.OrderBy(sysInfo => sysInfo.systemName).ToList();
        OnSystemRegistrationProcessCompleted?.Invoke();
    }

    void ReportStartUp()
    {
        if (systemInfoList.Where(systemInfo => systemInfo.systemStatus.systemStatus == Status.STOPPED).Any()) return;
        Debug.Log("All Systems Green!");
    }

    void StartUpAllSystems()
    {
        //We're going to turn on all systems defined in the game.
        //GetSystem<CurrencySystem>().Run();
        GetSystem<PlayerDataSerializationSystem>()?.Run();
        GetSystem<DeitySystem>()?.Run();
        GetSystem<StatsSystem>()?.Run();
        GetSystem<HealthSystem>()?.Run();
        GetSystem<ManaSystem>()?.Run();
        GetSystem<ExperienceSystem>()?.Run();
        GetSystem<ResurrectionSystem>()?.Run();
        GetSystem<SkillSystem>()?.Run();
        GetSystem<ItemSystem>()?.Run();
        GetSystem<MadoSystem>()?.Run();
        GetSystem<HeavensPlazaSystem>()?.Run();
        GetSystem<ActionSystem>()?.Run();
        GetSystem<AttackDefenseSystem>()?.Run();
        GetSystem<RuntimeActionSystem>()?.Run();

        OnSystemStartProcessCompleted?.Invoke();
    }

    internal T? WithSystem<T>() where T : GameSystem
    {
        return GetSystem<T>();
    }

    /// <summary>
    /// Gain access to the Game System defined in the game.
    /// </summary>
    /// <param name="_systemName">The name of the system.</param>
    /// <returns>A game system.</returns>
    private T? GetGameSystem<T>() where T : GameSystem
    {
        var data = from sys in systemInfoList where sys.system.GetType() == typeof(T) select sys;
        if (data.Any() == false)
        {
            Debug.Log(typeof(T) + "isn't an existing system. Why not creating one that derives from 'GameSystem'?");
            return null;
        }
        return (T)Convert.ChangeType(data.Single().system, typeof(T));
    }

    /// <summary>
    /// Gain access to rather the Game System is Running, Pause, or Stopped.
    /// </summary>
    /// <param name="_systemName"></param>
    /// <returns>A status of a specified game system.</returns>
    private SystemStatus? GetSystemStatus(string _systemName)
    {
        foreach (SystemInfo info in systemInfoList)
        {
            if (info.systemName == _systemName)
                return info.systemStatus;
        }
        Debug.Log(_systemName + " system does not exist.");
        return null;
    }

    /// <summary>
    /// Let's you retrieve all systems, no matter what their status are.
    /// </summary>
    /// <returns>All system statuses</returns>
    private SystemInfo[] GetAllSystemInfo()
    {
        var systemInfo = from system
                         in systemInfoList
                         select system;

        SystemInfo[] data = systemInfo.ToArray();

        return data;
    }

    /// <summary>
    /// Let's you retrieve all systems will a specific status.
    /// </summary>
    /// <param name="_status"></param>
    /// <returns>Systems with a specify status.</returns>
    private SystemInfo[] GetAllSystemInfo(Status _status)
    {
        var systemInfo = from system
                         in systemInfoList
                         where system.systemStatus.Retrieve() == _status
                         select system;

        SystemInfo[] data = systemInfo.ToArray();

        return data;
    }

    private void CreateProfileDirectory()
    {
        try
        {
            dirInfo?.Create();
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static void Save()
    {
        GameState ??= new(0);
        PlayerDataSerializationSystem.PlayerDataStateSet[ActiveProfileIndex].UpdateUniversalGameState(GameState);
    }

    public static void Load()
    {
        GameState = PlayerDataSerializationSystem.PlayerDataStateSet[ActiveProfileIndex].GetUniversalGameState();
    }
}