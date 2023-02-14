using System;

[Serializable]
public sealed class SettingValue<T>
{
    T _data;
    public T Data
    {
        get
        {
            return _data;
        }
    }

    public SettingValue(T value)
    {
        _data = value;
    }

    public static implicit operator SettingValue<T>(T value)
    {
        return new SettingValue<T>(value);
    }
}