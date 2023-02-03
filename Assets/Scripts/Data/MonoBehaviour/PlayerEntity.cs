#nullable enable

using System;
using UnityEngine;

using static SharedData.Constants;

public class PlayerEntity : MonoBehaviour, IHealthProperty, IManaProperty, ILevelProperty, IEntityStatus
{
    BossEntity? BossEntity => GameManager.Boss;

    [Header("Viewable Player Data")]
    [SerializeField] float HPValue;
    [SerializeField] float MaxHPValue;
    [SerializeField] float MPValue;
    [SerializeField] float MaxMPValue;
    [SerializeField] float LevelRankValue;
    [SerializeField] float XPValue;

    public Action? OnHealthPositiveChange { get; set; }
    public Action? OnHealthNegativeChange { get; set; }
    public Action? OnHealthDown { get; set; }

    [Header("Stats")]
    public EntityStats? stats;

    public float HealthValue
    {
        get { return HPValue; }
        set { HPValue = value; }
    }

    public float MaxHealthValue
    {
        get { return MaxHPValue; }
        set { MaxHPValue = value; }
    }

    public float ManaValue
    {
        get { return MPValue; }
        set { MPValue = value; }
    }

    public float MaxManaValue
    {
        get { return MaxMPValue; }
        set { MaxMPValue = value; }
    }

    public float LevelValue
    {
        get { return LevelRankValue; }
        set { LevelRankValue = value; }
    }

    public float ExperienceValue
    {
        get { return XPValue; }
        set { XPValue = value; }
    }

    public StanceState EntityStanceState { get; set; }
    public OffensiveState EntityOffensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }

    private Alarm alarm = new(DefaultAlarmSize);

    private const int PlayerStartingMaxHealth = 50;
    private const int PlayerStatingMaxMana = 100;

    private void Start()
    {
        GameManager.ReferencePlayer(this);

        // Health system can talk multiple, so we use this instead
        HealthSystem.AddNewEntry(nameof(PlayerEntity), this);

        //Set Max Values
        HealthSystem.SetMaxHealth(nameof(PlayerEntity), PlayerStartingMaxHealth);
        HealthSystem.SetHealth(nameof(PlayerEntity), PlayerStartingMaxHealth);
        ManaSystem.SetMaxMana(100);
        ManaSystem.SetMana(PlayerStatingMaxMana);

        AttackDefenseSystem.OnParry = EventManager.AddEvent(001, string.Empty, () =>
        {
            // Check Boss Entity's parry percentage range
            // If you did a parry between the range, the parry was a success
            if (BossEntity!.WasParryTimed) AttackDefenseSystem.OnParrySuccess.Trigger();
        });

        AttackDefenseSystem.OnPoiseLost = EventManager.AddEvent(002, string.Empty, () =>
        {
            EntityStanceState = StanceState.Stunned;
            Debug.Log("Oh no! Player is in stunned state for 5 seconds");
            alarm.SetFor(5f, Two, true, () =>
            {
                EntityStanceState = StanceState.Idle;
                Debug.Log("Hazzah! Player is out of stunned state!");
                AttackDefenseSystem.RestorePoise();
            });
        });
    }

    public void AddExperience()
    {
        ExperienceSystem.GainExperience();
        LevelValue = ExperienceSystem.CurrentLevel;
        XPValue = ExperienceSystem.CurrentExperience;
    }
}
