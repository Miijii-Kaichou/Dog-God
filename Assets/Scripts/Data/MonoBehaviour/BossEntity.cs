#nullable enable

using System;
using System.Collections;
using UnityEngine;

using static SharedData.Constants;
using Random = UnityEngine.Random;

public class BossEntity : MonoBehaviour, IHealthProperty, IEntityStatus
{
    private PlayerEntity? Target => GameManager.Player;

    [Header("Viewable Boss Data")]
    [SerializeField] float HPValue;
    [SerializeField] float MaxHPValue;

    // Boss Status
    public EntityStats? stats;

    // Motion percetage of attack.
    [SerializeField]
    private float motionPercentage = 0f;
    private (float, float) parryPercentageRange;

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

    private HealthSystem? _healthSystem;

    public StanceState EntityStanceState { get; set; }
    public OffensiveState EntityOffensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }
    public Action? OnHealthDown { get; set; }
    public Action? OnHealthNegativeChange {get;set;}
    public Action? OnHealthPositiveChange { get; set; }

    public bool WasParryTimed
    {
        get
        {
            return  EntityOffensiveState != OffensiveState.None && 
                    motionPercentage >= parryPercentageRange.Item1 &&
                    motionPercentage <= parryPercentageRange.Item2;
        }
    }

    private const int BossStartingMaxHealth = 1000000;

    private readonly Alarm? alarm = new(DefaultAlarmSize);
    private AttackDefenseSystem? _attackDefenseSystem;

    void Start()
    {
        GameManager.ReferenceBoss(this);

        HealthSystem.AddNewEntry(nameof(BossEntity), this);

        HealthSystem.SetHealth(nameof(BossEntity), BossStartingMaxHealth);
        HealthSystem.SetMaxHealth(nameof(BossEntity), BossStartingMaxHealth);

        // Test
        AttackDefenseSystem.OnParrySuccess = EventManager.AddEvent(101, "GotParriedEvent", () =>
        {
            Debug.Log("You parried Inugami Koko successfully. Stunned for 5 Seconds");
            EntityStanceState = StanceState.Stunned;
            alarm?.SetFor(5, Two, true, () => {
                EntityStanceState = StanceState.Idle;
                Debug.Log("Watch out! Inugami Koko is out of stunned state.");
            });
        });

        // We'll now randomize 
        alarm?.SetFor(Random.Range(1, 3), Zero, false, () =>
        {
            if (EntityStanceState == StanceState.Stunned) return;
            StartCoroutine(DoNormalAttack());
            alarm[Zero].SetDuration = Random.Range(1, 3);
            alarm[Zero].CurrentTime = Zero;
        });
    }

    IEnumerator DoNormalAttack()
    {
        EntityStanceState = StanceState.Offensive;

        alarm?.SetFor(Random.Range(0.25f, 1f), One, true, () =>
        {
            alarm[One].CurrentTime = 0;
            if (EntityStanceState == StanceState.Stunned) return;
            SendDamageToPlayer(Random.Range(1, 10));
            EntityStanceState = StanceState.Idle;
            EntityOffensiveState = OffensiveState.None;
            motionPercentage = Zero;
        });
    
        while(motionPercentage < 0.99f)
        {
            EntityOffensiveState = OffensiveState.Attack;
            motionPercentage = alarm![One].CurrentTime / alarm[One].SetDuration;
            parryPercentageRange = (0.49f, 0.91f);
            yield return new WaitForSeconds(MaxTime);
        }
    }


    void SendDamageToPlayer(float damageValue)
    {
        if (Target!.EntityDefensiveState == DefensiveState.Guard)
        {
            AttackDefenseSystem.LosePoise(damageValue);
            damageValue /= 10;
        }
        HealthSystem.SetHealth(nameof(PlayerEntity), -damageValue, isRelative: true);
    }
}