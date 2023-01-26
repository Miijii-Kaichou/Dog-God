using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
