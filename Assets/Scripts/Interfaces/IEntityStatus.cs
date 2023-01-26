public interface IEntityStatus
{
    public StanceState EntityStanceState { get; set; }
    public OffensiveState EntityOffsensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }

    public void ChangeStanceState(StanceState newState)
    {
        if (EntityStanceState == newState) return;
        EntityStanceState = newState;
    }
    public void ChangeOffensiveState(OffensiveState newState)
    {
        if (EntityOffsensiveState == newState) return;
        EntityOffsensiveState = newState;
    }
    public void ChangeDefensiveState(DefensiveState newState)
    {
        if (EntityDefensiveState == newState) return;
        EntityDefensiveState = newState;
    }
}