using Cakewalk.IoC;

[System.Serializable]
public class SystemInfo
{
    public string systemName;
    [Dependency] public GameSystem system;
    [Dependency] public SystemStatus systemStatus;
}