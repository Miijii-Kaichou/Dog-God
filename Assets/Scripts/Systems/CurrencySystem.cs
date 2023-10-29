using System;
using static SharedData.Constants;

public sealed class CurrencySystem : GameSystem
{
    private static CurrencySystemState _SystemState;
    private static CurrencySystem _Self;

    public int Currency { get; private set; } = 0;

    public static EventCall OnCurrencyAdd, OnCurrencyTake;

    protected override void OnInit()
    {
        _Self = GameManager.GetSystem<CurrencySystem>();
        InitializeSystemState();
    }

    private void InitializeSystemState()
    {
        _SystemState = new(_Self.Currency);
    }

    private void Start()
    {
        OnCurrencyAdd ??= EventManager.AddEvent(EVT_OnCurrencyAdd, string.Empty);
        OnCurrencyTake ??= EventManager.AddEvent(EVT_OnCurrencyTake, string.Empty);
    }

    public static void AddToCurrency(int value)
    {
        _Self.Currency += value;
        OnCurrencyAdd.Trigger();
    }

    public static void TakeFromCurrency(int value)
    {
        _Self.Currency -= value;
        OnCurrencyTake.Trigger();
    }

    public static bool IsSufficientFunds(int valueOfPurchase)
    {
        return _Self.Currency >= valueOfPurchase;
    }

    public static void Save()
    {
        _SystemState = new(_Self.Currency);
        PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].UpdateCurrencyStateData(_SystemState);
    }

    public static void Load()
    {
        _SystemState = PlayerDataSerializationSystem.PlayerDataStateSet[GameManager.ActiveProfileIndex].GetCurrencyStateData();
        _Self.Currency = _SystemState.Currency;
    }
}
