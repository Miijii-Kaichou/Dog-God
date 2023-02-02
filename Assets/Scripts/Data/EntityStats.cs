using static SharedData.Constants;

public sealed class EntityStats
{
    int[] stats = new int[MaxStatsSize] { 0, 0, 0, 0, 0, 0, 0 };
    
    public int this[StatVariable statVariable]
    {
        get
        {
            return stats[(int)statVariable];
        }
        set
        {
            stats[(int)statVariable] = value;
        }
    }
}