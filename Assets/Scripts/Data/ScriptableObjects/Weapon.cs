using UnityEngine;

public abstract class Weapon : ScriptableObject, IActionableItem
{
    public string itemName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Quantity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool AllowQuantityResize => throw new System.NotImplementedException();

    public int SlotNumber { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public ItemUseCallaback OnItemUse => throw new System.NotImplementedException();

    //The description or lore of the weapon.
    [TextArea(minLines: 1, maxLines: 10)]
    public string caption;

    //Weapon stats (these states will be added to the players)
    public WeaponStats stats;

    //Requirements that the player has to have in order to wield it.
    public Requirement[] requirements;

    //Damage Type
    public enum WeaponDamageType
    {
        NONE = 0,
        BURNING = 1 << 0,
        FREEZING = 1 << 1,
        POISON = 1 << 2,
        LIGHTNING = 1 << 3,
        LIGHT = 1 << 4,
        DARK = 1 << 5,
        ONEHAND = 1 << 6,
        TWOHAND = 1 << 7,
        DUALWIELD = 1 << 8
    }

    public WeaponDamageType weaponDamageType;

    public void Test()
    {
        //Assuming that our straight sword has One-Hand damage, we can also make it take Lightning damage as well.
        weaponDamageType |= WeaponDamageType.LIGHTNING;

        if ((weaponDamageType & WeaponDamageType.LIGHTNING) == WeaponDamageType.LIGHTNING)
        {
            //This means that the bit in our weapons is 1,
            //meaning that our weapon does add lightning damage when
            //in use.
        }

        //We don't like our build. We'll remove lightning damage from our
        //straight sword.
        weaponDamageType &= ~WeaponDamageType.LIGHTNING;

        //We'll toggle it on or off.
        weaponDamageType ^= WeaponDamageType.LIGHTNING;
    }
}
