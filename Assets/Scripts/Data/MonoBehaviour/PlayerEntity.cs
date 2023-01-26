using UnityEngine;

public class PlayerEntity : MonoBehaviour, IHealthProperty, IManaProperty, ILevelProperty, IEntityStatus
{
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
    public OffensiveState EntityOffsensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }


    // Dependencies
    private AttackDefenseSystem _attackDefenseSystem;
    private ActionSystem _playerActionSystem;
    private LevelingSystem _levelingSystem;
    private HealthSystem _healthSystem;
    private ManaSystem _manaSystem;

    private void Start()
    {
        MaxHealthValue = 50000;
        HealthValue = MaxHealthValue;
        MaxManaValue = 1000;
        ManaValue = MaxManaValue;

        _attackDefenseSystem ??= GameManager.Command.GetSystem<AttackDefenseSystem>();
        _playerActionSystem ??= GameManager.Command.GetSystem<ActionSystem>();
        _levelingSystem ??= GameManager.Command.GetSystem<LevelingSystem>();
        _healthSystem ??= GameManager.Command.GetSystem<HealthSystem>();
        _manaSystem ??= GameManager.Command.GetSystem<ManaSystem>();

        // Register player reference to systems
        ((IRegisterPlayer<PlayerEntity>)_attackDefenseSystem).RegisterPlayerEntity(this);
        ((IRegisterPlayer<ILevelProperty>)_levelingSystem).RegisterPlayerEntity(this);
        ((IRegisterPlayer<IManaProperty>)_manaSystem).RegisterPlayerEntity(this);

        // Health system can talk multiple, so we use this instead
        _healthSystem.AddNewEntry("PlayerEntity", this);

        //Set Max Values
        _healthSystem.SetMaxHealth("PlayerEntity", 50000);
        _manaSystem.SetMaxMana(1000);
    }

    public void AddHealth(float value)
    {
        throw new System.NotImplementedException();
    }

    public void AddToMana(float value)
    {
        throw new System.NotImplementedException();
    }

    public void AddExperience()
    {
        _levelingSystem.GainExperience();
        LevelValue = _levelingSystem.CurrentLevel;
        XPValue = _levelingSystem.CurrentExperience;
    }
}
