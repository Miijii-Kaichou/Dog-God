public class State<T> where T : new()
{
    public T value;
    public static State<T> New(T newValue)
    {
        return new State<T>()
        {
            value = newValue
        };
    }
}
