interface IUseCondition
{
    // Call the Use Method only if a set condition has been met. This enforces that
    public Condition UseCondition { get; }
    void OnConditionMet() { }
}
