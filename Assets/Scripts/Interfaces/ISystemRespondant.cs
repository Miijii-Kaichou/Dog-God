internal interface ISystemRespondant<T> where T : GameSystem
{
    public void OnSystemReady(T gamesystem);
}