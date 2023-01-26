[System.Serializable]
public struct WeaponStatProperty
{
    /*StatProperty has a name of that property, as well as a min and max value.
     If a min is just give, a random number will be generated from the min and min + 10.*/
    public DamageStatVariable property;

    public int value;
}
