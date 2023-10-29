using static SharedData.Constants;

public sealed class ResurrectionSystem : GameSystem
{
    private static ResurrectionSystemState _SystemState;
    private static ResurrectionSystem _Self;

    public int Lives { get; private set; } = 3;

    private static EventCall OnDeath, OnCease, OnRestore;

    protected override void OnInit()
    {
        _Self = GameManager.GetSystem<ResurrectionSystem>();

        OnDeath ??= EventManager.AddEvent(EVT_OnDeath, string.Empty);
        OnCease ??= EventManager.AddEvent(EVT_OnCease, string.Empty);
        OnRestore ??= EventManager.AddEvent(EVT_OnRestore, string.Empty);
    }

    protected override void Main()
    {
        if (_Self.Lives < One)
        {
            OnCease.Trigger();
        }
    }

    public static void RestoreResurrectionCount()
    {
        _Self.Lives = DefaultResurrectionCount;
        OnRestore.Trigger();
    }

    public static void Die()
    {
        _Self.Lives--;
        OnDeath.Trigger();
    }

    public static void Save()
    {
        _SystemState = new(_Self.Lives);
        PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].UpdateResurrectionStateData(_SystemState);
    }

    public static void Load()
    {
        _SystemState = PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].GetResurrectionStateData();
        _Self.Lives = _SystemState.Lives;
    }
}
