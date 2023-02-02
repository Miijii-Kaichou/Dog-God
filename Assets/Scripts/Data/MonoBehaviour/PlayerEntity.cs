using System;
using UnityEngine;

using static SharedData.Constants;

public class PlayerEntity : MonoBehaviour, IHealthProperty, IManaProperty, ILevelProperty, IEntityStatus
{
    [Header("Target")]
    [SerializeField] BossEntity bossEntity;

    [Header("Viewable Player Data")]
    [SerializeField] float HPValue;
    [SerializeField] float MaxHPValue;
    [SerializeField] float MPValue;
    [SerializeField] float MaxMPValue;
    [SerializeField] float LevelRankValue;
    [SerializeField] float XPValue;

    [Header("Stats")]
    public EntityStats stats;

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
    public Action OnHealthDown { get; set; }

    // Dependencies
    private AttackDefenseSystem _attackDefenseSystem;
    private ActionSystem _playerActionSystem;
    private LevelingSystem _levelingSystem;
    private HealthSystem _healthSystem;
    private ManaSystem _manaSystem;
    private Alarm alarm = new(DefaultAlarmSize);

    private const int PlayerStartingMaxHealth = 50;
    private const int PlayerStatingMaxMana = 100;

    private void Start()
    {
        GameManager.ReferencePlayer(this);

        _attackDefenseSystem ??= GameManager.GetSystem<AttackDefenseSystem>();
        _playerActionSystem ??= GameManager.GetSystem<ActionSystem>();
        _levelingSystem ??= GameManager.GetSystem<LevelingSystem>();
        _healthSystem ??= GameManager.GetSystem<HealthSystem>();
        _manaSystem ??= GameManager.GetSystem<ManaSystem>();

        // Register player reference to systems
        ((IRegisterEntity<PlayerEntity>)_attackDefenseSystem).RegisterEntity(this);
        ((IRegisterEntity<ILevelProperty>)_levelingSystem).RegisterEntity(this);
        ((IRegisterEntity<IManaProperty>)_manaSystem).RegisterEntity(this);

        // Health system can talk multiple, so we use this instead
        _healthSystem.AddNewEntry(nameof(PlayerEntity), this);

        //Set Max Values
        _healthSystem.SetMaxHealth(nameof(PlayerEntity), PlayerStartingMaxHealth);
        _healthSystem.SetHealth(nameof(PlayerEntity), PlayerStartingMaxHealth);
        _manaSystem.SetMaxMana(100);
        _manaSystem.SetMana(PlayerStatingMaxMana);

        _attackDefenseSystem.onParry = EventManager.AddEvent(001, string.Empty, () =>
        {
            // Check Boss Entity's parry percentage range
            // If you did a parry between the range, the parry was a success
            if (bossEntity.WasParryTimed) _attackDefenseSystem.onParrySuccess.Trigger();
        });

        _attackDefenseSystem.onPoiseLost = EventManager.AddEvent(002, string.Empty, () =>
        {
            EntityStanceState = StanceState.Stunned;
            Debug.Log("Oh no! Player is in stunned state for 5 seconds");
            alarm.SetFor(5f, Two, true, () =>
            {
                EntityStanceState = StanceState.Idle;
                Debug.Log("Hazzah! Player is out of stunned state!");
                _attackDefenseSystem.RestorePoise();
            });
        });
    }

    public void AddExperience()
    {
        _levelingSystem.GainExperience();
        LevelValue = _levelingSystem.CurrentLevel;
        XPValue = _levelingSystem.CurrentExperience;
    }
}
