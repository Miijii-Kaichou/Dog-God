#nullable enable
public interface IRegisterEntity<T> where T : class
{
    public T? EntityReference { get; set; }
    public void RegisterEntity(T target) { EntityReference = target; }
}