using System;
using UnityEngine;

#nullable enable

public class LevelingSystem : GameSystem, IRegisterPlayer<ILevelProperty>
{
    /*So, the Leveling System takes in all the things that has
    LV. All of this information will be displayed in game, keeping track
    of everyone's health, that includes the Player's LV.

    There will also be a UI portion of the system as well.*/
    public delegate void LevelSystemOperation();

    public LevelSystemOperation? onLevelChange;
    public LevelSystemOperation? onExperienceChange;

    float previousLevel = DefaultLevel;

    float currentLevel = DefaultLevel;
    public float CurrentLevel => currentLevel;

    float currentExperience = DefaultExperience;
    public float CurrentExperience => currentExperience;
    public float CurrentExperiencePoints => Mathf.RoundToInt(experienceTilNextLevel * (currentExperience % 1));

    float experienceTilNextLevel = DefaultMaxExperience;
    public float ExperienceTilNextLevel => experienceTilNextLevel;
    private float experienceGainValue;

    const float MaxLevel = 100;
    const float DefaultLevel = 1;
    const float DefaultExperience = 0;
    const float DefaultMaxExperience = 42;
    private const int Two = 2;

    public ILevelProperty? EntityRef { get; set; }
    public Action? OnLevelUp { get; internal set; }

    public PlayerHUD? PlayerHUD;
    private float targetExperience;
    private float experienceVelocity;

    protected override void OnInit()
    {
        base.OnInit();
    }

    protected override void Main()
    {
        currentExperience = Mathf.SmoothDamp(currentExperience, targetExperience, ref experienceVelocity, 0.1f);
        currentLevel = Mathf.FloorToInt(DefaultLevel + (currentExperience / DefaultLevel));
        ListenForLevelUp();
    }

    void UpdateLevel()
    {
        experienceTilNextLevel = Mathf.RoundToInt(experienceTilNextLevel / Mathf.Log(Two));

    }

    void ListenForLevelUp()
    {
        if (currentLevel != previousLevel)
        {
            previousLevel = currentLevel;
            UpdateLevel();
            onLevelChange?.Invoke();
        }
    }

    public void GainExperience()
    {
        UpdateExperienceGainValue();
        targetExperience += experienceGainValue;
        onExperienceChange?.Invoke();
    }

    void UpdateExperienceGainValue()
    {
        experienceGainValue = DefaultMaxExperience / (DefaultMaxExperience * 10 * currentLevel);
    }
}