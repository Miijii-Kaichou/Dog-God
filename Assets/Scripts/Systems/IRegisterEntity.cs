#nullable enable
public interface IRegisterEntity<T> where T : class
{
    public T? _player { get; set; }
    public void RegisterEntity(T target) { _player = target; }
}