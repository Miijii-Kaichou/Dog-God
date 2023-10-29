using System;

[Serializable]
public sealed class PlayerDataState
{
    public int dataID = 0;

    public UniversalGameState gameState;

    public HealthSystemState healthSystem;
    public ManaSystemState manaSystem;
    public ExperienceSystemState experienceSystem;
    public StatsSystemState statSystem;

    public ItemSystemState itemSystem;
    public SkillSystemState skillSystem;
    public MadoSystemState madoSystem;
    public DeitySystemState deitySystem;

    public CurrencySystemState currencySystem;
    public ResurrectionSystemState resurrectionSystem;

    internal ProfileStatus Status { get; set; } = ProfileStatus.Empty;

    public PlayerDataState()
    {
        GameManager.OnSystemStartProcessCompleted += () =>
        {
            dataID = PlayerDataSerializationSystem.PlayerDataStateSet.Length;
        };
    }

    public void UpdateUniversalGameState(UniversalGameState universalGameState) => gameState = universalGameState;
    public void UpdateProfileStatus(ProfileStatus status) => Status = status;
    public void UpdateStatStateData(StatsSystemState instance) => statSystem = instance;
    public void UpdateItemStateData(ItemSystemState instance) => itemSystem = instance;
    public void UpdateSkillStateData(SkillSystemState instance) => skillSystem = instance;
    public void UpdateMadoStateData(MadoSystemState instance) => madoSystem = instance;
    public void UpdateDeityStateData(DeitySystemState instance) => deitySystem = instance;
    public void UpdateHealthStateData(HealthSystemState instance) => healthSystem = instance;
    public void UpdateManaStateData(ManaSystemState instance) => manaSystem = instance;
    public void UpdateExperienceStateData(ExperienceSystemState instance) => experienceSystem = instance;
    public void UpdateCurrencyStateData(CurrencySystemState instance) => currencySystem = instance;
    public void UpdateResurrectionStateData(ResurrectionSystemState instance) => resurrectionSystem = instance;

    public UniversalGameState GetUniversalGameState() => gameState;
    public StatsSystemState GetStatsStateData() => statSystem;
    public ItemSystemState GetItemStateData() => itemSystem;
    public SkillSystemState GetSkillStateData() => skillSystem;
    public MadoSystemState GetMadoStateData() => madoSystem;
    public DeitySystemState GetDeityStateData() => deitySystem;
    public HealthSystemState GetHealthStateData() => healthSystem;
    public ManaSystemState GetManaStateData() => manaSystem;
    public ExperienceSystemState GetExperienceStateData() => experienceSystem;
    public CurrencySystemState GetCurrencyStateData() => currencySystem;
    public ResurrectionSystemState GetResurrectionStateData() => resurrectionSystem;
}