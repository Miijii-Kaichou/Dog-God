using System;

public class TargetInfo<T> where T : IActionableItem
{
    public Type TargetType { get; private set; }

    public static TargetInfo<T> Target()
    {
        return new TargetInfo<T> { TargetType = typeof(T) };
    }

    public static implicit operator TargetInfo<Skill>(TargetInfo<T> info)
    {
        return info;
    }

    public static implicit operator TargetInfo<Item>(TargetInfo<T> info)
    {
        return info;
    }
}
