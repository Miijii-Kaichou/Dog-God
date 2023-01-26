using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotNumber {
    SLOTONE,
    SLOTTWO
}

public enum DeitySlotNumber
{
    SLOTONE,
    SLOTTWO,
    SLOTTHREE,
    SLOTFOUR,
    SLOTFIVE,
    SLOTSIX,
    SLOTSEVEN
}


public class InGameSlots : MonoBehaviour
{
    private static InGameSlots Instance;

    /*InGameSlots is just an object that utilizes the equippable items that can be put on
     for use!
     For example, one of the top 2 slots are for weapons. Either side can be the sword or the shield.
     The bottom two are reserved for items that can be quickly used. This will be used on your turn though, so
     think out your strategy wisely.*/

    [SerializeField]
    private Weapon[] weaponSlots = new Weapon[2];

    [SerializeField]
    private Item[] itemSlots = new Item[2];

    [SerializeField]
    private Deity[] deitySlots = new Deity[7];

    const object NOTASSIGNED = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void AddToWeaponSlot(SlotNumber _slotNumber, Weapon _weapon)
    {
        Instance.weaponSlots[(int)_slotNumber] = _weapon;
    }

    public static void RemoveFromWeaponSlot(SlotNumber _slotNumber)
    {
        Instance.weaponSlots[(int)_slotNumber] = (Weapon)NOTASSIGNED;
    }

    public static void AddToItemSlot(SlotNumber _slotNumber, Item _item)
    {
        Instance.itemSlots[(int)_slotNumber] = _item;
    }

    public static void RemoveFromItemSlot(SlotNumber _slotNumber)
    {
        Instance.itemSlots[(int)_slotNumber] = (Item)NOTASSIGNED;
    }

    public static void AssignDeitySlot(DeitySlotNumber _slotNumber, Deity _deity)
    {
        Instance.deitySlots[(int)_slotNumber] = _deity;
    }

    public static void RemoveDeitySlot(DeitySlotNumber _slotNumber)
    {
        Instance.deitySlots[(int)_slotNumber] = (Deity)NOTASSIGNED;
    }
}
