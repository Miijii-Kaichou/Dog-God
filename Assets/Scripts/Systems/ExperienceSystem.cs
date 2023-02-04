using System;
using UnityEngine;

using static SharedData.Constants;

#nullable enable

public class ExperienceSystem : GameSystem
{   
    public static ExperienceSystem? Self;
    /*So, the Leveling System takes in all the things that has
    LV. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's LV.

    There will also be a UI portion of the system as well.*/
    
    public static PlayerHUD? PlayerHUD;
    
    public delegate void LevelSystemOperation();

    public static LevelSystemOperation? onLevelChange;
    public static LevelSystemOperation? onExperienceChange;

    float previousLevel = DefaultLevel;

    private float currentLevel = DefaultLevel;
    public static float CurrentLevel => Self!.currentLevel;

    private float currentExperience = DefaultExperience;
    public static float CurrentExperience => Self!.currentExperience;
    public static float CurrentExperiencePoints => Mathf.RoundToInt(Self!.experienceTilNextLevel * (Self.currentExperience % 1));

    private float experienceTilNextLevel = DefaultMaxExperience;
    public static float ExperienceTilNextLevel => Self!.experienceTilNextLevel;
    private static float experienceGainValue;

    const float DefaultLevel = 1;
    const float DefaultExperience = 0;
    const float DefaultMaxExperience = 42;
    private const int Two = 2;

    public static ILevelProperty? PlayerLevelProp => Self!.Player;
    public static Action? OnLevelUp { get; internal set; }

    private static float TargetExperience;
    private static float TargetLevel = 0;
    private static float ExperienceVelocity;

    protected override void OnInit()
    {
        Self = GameManager.GetSystem<ExperienceSystem>();
    }

    protected override void Main()
    {
        currentExperience = Mathf.SmoothDamp(currentExperience, TargetExperience, ref ExperienceVelocity, 0.1f);
        currentLevel = Mathf.FloorToInt(DefaultLevel + (currentExperience / DefaultLevel));
        if(TargetLevel != Zero) GainExperience();
        ListenForLevelUp();

    }

    private static void UpdateLevel()
    {
        Self!.experienceTilNextLevel = Mathf.RoundToInt(Self.experienceTilNextLevel / Mathf.Log(Two));
        TargetLevel -= TargetLevel > Zero ? One : Zero;
    }

    private static void ListenForLevelUp()
    {
        if (Self!.currentLevel != Self.previousLevel)
        {
            Self.previousLevel = Self.currentLevel;
            UpdateLevel();
            onLevelChange?.Invoke();
        }
    }

    public static void GainExperience()
    {
        UpdateExperienceGainValue();
        TargetExperience += experienceGainValue;
        onExperienceChange?.Invoke();
    }

    private static void UpdateExperienceGainValue()
    {
        experienceGainValue = DefaultMaxExperience / (DefaultMaxExperience * 10 * Self!.currentLevel);
    }

    internal static void UpTotalLevelsMore(float levelGain)
    {
        // Just setting this will automatically trigger
        // a level gain cycle. The Main Cycle will subtract on each
        // level up until it hits 0 again.
        // BTW. this stacks (it sort of needs to be).
        TargetLevel += levelGain;
    }
}