#nullable enable
public interface IRegisterPlayer<T> where T : class
{
    public T? EntityRef { get; set; }
    public void RegisterPlayerEntity(T player) { EntityRef = player; }
}