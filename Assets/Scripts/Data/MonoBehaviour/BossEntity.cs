using UnityEngine;

public class BossEntity : MonoBehaviour, IHealthProperty, IEntityStatus
{
    [Header("Viewable Boss Data")]
    [SerializeField] float HPValue;
    [SerializeField] float MaxHPValue;

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

    private HealthSystem _healthSystem;

    public StanceState EntityStanceState { get; set; }
    public OffensiveState EntityOffsensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }

    void Start()
    {
        _healthSystem ??= GameManager.Command.GetSystem<HealthSystem>();

        _healthSystem.AddNewEntry(nameof(BossEntity), this);

        _healthSystem.SetHealth(nameof(BossEntity), 1000000);
        _healthSystem.SetMaxHealth(nameof(BossEntity), 100000);
    }
}