using System;

[Serializable]
public sealed class CurrencySystemState
{
    public CurrencySystemState(int currency)
    {
        Currency = currency;
    }

    public int Currency { get; }
}