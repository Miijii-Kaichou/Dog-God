using System;

[Serializable]
public sealed class PlayerDataState
{
    public int dataID = 0;

    public PlayerEntityState playerEntity;
    public StatsSystemState statSystem;
    public ItemSystemState itemSystem;
    public SkillSystemState skillSystem;
    public MadoSystemState madoSystem;
    public DeitySystemState deitySystem;

    internal ProfileStatus Status { get; set; } = ProfileStatus.Empty;

    public PlayerDataState()
    {
        GameManager.OnSystemStartProcessCompleted += () =>
        {
            dataID = PlayerDataSerializationSystem.PlayerDataStateSet.Length;
        };
    }

    public void UpdateProfileStatus(ProfileStatus status) => Status = status;
    public void UpdatePlayerEntityStateData(PlayerEntityState instance) => playerEntity = instance;
    public void UpdateStatStateData(StatsSystemState instance) => statSystem = instance;
    public void UpdateItemStateData(ItemSystemState instance) => itemSystem = instance;
    public void UpdateSkillStateData(SkillSystemState instance) => skillSystem = instance;
    public void UpdateMadoStateData(MadoSystemState instance) => madoSystem = instance;
    public void UpdateDeityStateData(DeitySystemState instance) => deitySystem = instance;

    public PlayerEntityState GetPlayerEntityStateData() => playerEntity;
    public StatsSystemState GetStatsSystemStateData() => statSystem;
    public ItemSystemState GetItemSystemStateData() => itemSystem;
    public SkillSystemState GetSkillSystemStateData() => skillSystem;
    public MadoSystemState GetMadoSystemStateData() => madoSystem;
    public DeitySystemState GetDeitySystemStateData() => deitySystem;
}