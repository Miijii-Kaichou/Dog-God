/// <summary>
/// Defines the status of a system.
/// </summary>
public enum Status
{
    STOPPED,
    PAUSED,
    RUNNING
}

[System.Serializable]
public class SystemStatus
{
    /*This will be a very handy thing that will display on the GameManager. This will keep track on all
     the systems that this game uses in order to operate. Perhaps if this is implemented correctly, you can
     find where issues may be occuring...*/

    public Status systemStatus;

    public void OnStatusChange(Status _systemStatus)
    {
        systemStatus = _systemStatus;
    }

    public Status Retrieve() => systemStatus;
}
