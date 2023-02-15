using System;
using static SharedData.Constants;

[Serializable]
public sealed class EntityStats
{
    int[] _stats = new int[MaxStatsSize] { 0, 0, 0, 0, 0, 0, 0 };
    
    public int this[StatVariable statVariable]
    {
        get
        {
            return _stats[(int)statVariable];
        }
        set
        {
            _stats[(int)statVariable] = value;
        }
    }

    public int Size => _stats.Length;
}