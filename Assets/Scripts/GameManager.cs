using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    [SerializeField, Header("Sprite Atlas")]
    SpriteAtlas spriteAtlas;

    public static SpriteAtlas SpriteAtlas
    {
        get { return Instance.spriteAtlas; }
    }


    public static T GetSystem<T>() where T : GameSystem => Instance.GetGameSystem<T>();
    public static SystemStatus GetSystemStatus(GameSystem system) => Instance.GetSystemStatus(system.SystemName);
    public static SystemInfo[] GetSystemInfo() => Instance.GetAllSystemInfo();
    public static SystemInfo[] GetSystemInfo(Status _status) => Instance.GetAllSystemInfo(_status);


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

    DirectoryInfo dirInfo;

    [Header("Achievements"), SerializeField]
    private List<Achievement> achievements = new List<Achievement>();


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
    }

    // Start is called before the first frame update
    void Start()
    {

        Screen.SetResolution((int)resolutionWidth, (int)resolutionHeight, FullScreenMode.FullScreenWindow, Application.targetFrameRate);

        //I want to also create a folder of Profiles if one exists or not.

        dirInfo = new DirectoryInfo(Application.persistentDataPath + "/Profiles");

        if (!dirInfo.Exists)
            CreateProfileDirectory();

        StartUpAllSystems();
        ReportStartUp();
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
        GetSystem<DeitySystem>().Run();
        GetSystem<HealthSystem>().Run();
        GetSystem<ManaSystem>().Run();
        GetSystem<LevelingSystem>().Run();
        GetSystem<ResurrectionSystem>().Run();
        GetSystem<SkillSystem>().Run();
        GetSystem<ItemSystem>().Run();
        GetSystem<WeaponSystem>().Run();
        GetSystem<HeavensPlazaSystem>().Run();
        GetSystem<ActionSystem>().Run();
        GetSystem<AttackDefenseSystem>().Run();
        GetSystem<RuntimeActionSystem>().Run();
    }

    internal T WithSystem<T>() where T : GameSystem
    {
        return GetSystem<T>();
    }

    public void Goto(string _scene)
    {
        try
        {
            SceneManager.LoadScene(_scene);
        }
        catch (IOException e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    /// <summary>
    /// Gain access to the Game System defined in the game.
    /// </summary>
    /// <param name="_systemName">The name of the system.</param>
    /// <returns>A game system.</returns>
    private T GetGameSystem<T>() where T : GameSystem
    {
        var data = from sys in systemInfoList where sys.system.GetType() == typeof(T) select sys;
        if (data.Any() == false)
        {
            Debug.Log(typeof(T) + "isn't an existing system. Why not creating one that derives from 'GameSystem'?");
            return default;
        }
        return (T)Convert.ChangeType(data.Single(), typeof(T));
    }

    /// <summary>
    /// Gain access to rather the Game System is Running, Pause, or Stopped.
    /// </summary>
    /// <param name="_systemName"></param>
    /// <returns>A status of a specified game system.</returns>
    private SystemStatus GetSystemStatus(string _systemName)
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
            dirInfo.Create();
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
        }
    }

    private Achievement[] GetAllMarkedAchievements()
    {
        //Going to filter out the achievements that has only been completed
        return (from markedAchievement
                in achievements
                where markedAchievement.GetAchievementStatus() == true
                select markedAchievement).ToArray();
    }

    private Achievement[] GetAchievementsInASpanOf(double _days)
    {
        //Depending on how far you want the days to be, you'll get that total amount of achievements
        return (from markedAchievement
                in achievements
                where markedAchievement.GetDaysSinceAchievement() <= _days
                select markedAchievement).ToArray();
    }

    private Achievement[] SortAchievementsBy(SortingOrder sortingOrder, bool _descendingOrder = false)
    {
        Achievement[] sortedAchievements = null;
        //Sort by a specified SortingOrder
        switch (sortingOrder)
        {
            #region Sort by Title
            //Sort Achievemented by Title
            case SortingOrder.TITLE:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetTitle()
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;

            #endregion

            #region Sort by Date
            //Sort Achievemented by Date
            case SortingOrder.DATE:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetReceivalDate()
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;
            #endregion

            #region Sort by Total Days
            //Sort Achievemented by Days
            case SortingOrder.DAYS:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetReceivalDate().TimeOfDay.TotalDays
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;
            #endregion

            #region Sort by Total Hours
            //Sort Achievemented by Hours
            case SortingOrder.HOURS:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetReceivalDate().TimeOfDay.TotalHours
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;
            #endregion

            #region Sort by Total Minutes
            //Sort Achievemented by Minutes
            case SortingOrder.MINUTES:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetReceivalDate().TimeOfDay.TotalMinutes
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;
            #endregion

            #region Sort by Total Seconds
            //Sort Achievemented by Seconds
            case SortingOrder.SECONDS:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetReceivalDate().TimeOfDay.TotalSeconds
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;
            #endregion

            #region Sort by Rarity
            //Sort Achievemented by Rarity
            case SortingOrder.RARITY:
                sortedAchievements = (from markedAchievement
                                      in achievements
                                      orderby markedAchievement.GetAchievementRarity()
                                      select markedAchievement).ToArray();

                //Change sorting if set to true
                return _descendingOrder == true ? sortedAchievements.OrderByDescending(newSortedAchievement => sortedAchievements).ToArray() : sortedAchievements;
            #endregion

            #region Default
            default: return null;
                #endregion

        }
    }
}