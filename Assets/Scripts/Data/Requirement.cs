[System.Serializable]
public struct Requirement
{
    /*This is the required stats that must be acquired by the
     player in order to wield the weapon, either it'd be one-handed,
     two-handed, or dual-wielding.*/

    public StatVariable stat;
    public uint statVal;
}
