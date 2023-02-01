using static SharedData.Constants;
public delegate void ItemUseCallaback();

#nullable enable
public interface IActionableItem
{
    public int SlotNumber { get; set; }
    public abstract ItemUseCallaback? OnActionUse { get; }

    void UseAction()
    {
        OnActionUse?.Invoke();
    }
}