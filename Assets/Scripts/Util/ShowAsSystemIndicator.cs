using UnityEngine;

public class ShowAsSystemIndicator : PropertyAttribute
{
    SystemInfo _systemInfo;
    public SystemInfo systemInfo
    {
        get
        {
            return _systemInfo;
        }
    }
    public ShowAsSystemIndicator() { _systemInfo = new SystemInfo(); }
}
