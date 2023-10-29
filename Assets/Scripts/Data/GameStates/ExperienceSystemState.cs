using System;
#nullable enable

[Serializable]
public sealed class ExperienceSystemState
{
    public ExperienceSystemState(float currentLevel, float currentExperience, float experienceTilNextLevel)
    {
        CurrentLevel = currentLevel;
        CurrentExperience = currentExperience;
        ExperienceTilNextLevel = experienceTilNextLevel;
    }

    public float CurrentLevel { get; }
    public float CurrentExperience { get; }
    public float ExperienceTilNextLevel { get; }
}
