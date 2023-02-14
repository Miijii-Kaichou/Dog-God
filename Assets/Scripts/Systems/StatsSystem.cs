#nullable enable

public sealed class StatsSystem : GameSystem
{
    private static StatsSystem? Self;
    static EntityStats? StatState;
    static int ActiveProfile => GameManager.ActiveProfileIndex;

    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<StatsSystem>();
    }

    public static void Save()
    {
        PlayerDataSerializationSystem.PlayerDataStateSet[ActiveProfile].UpdateStatStateData(Self);
    }

    public static void Load()
    {
        Self = PlayerDataSerializationSystem.PlayerDataStateSet[ActiveProfile].GetStatsSystemStateData();
    }

    public static void SetPlayerStatsState(EntityStats newState)
    {
        StatState = newState; 
    }

    public static EntityStats? GetPlayerStatsState()
    {
        return StatState;
    }
}