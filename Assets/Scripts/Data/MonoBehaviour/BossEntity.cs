using System;
using System.Collections;
using UnityEngine;

using static SharedData.Constants;
using Random = UnityEngine.Random;

public class BossEntity : MonoBehaviour, IHealthProperty, IEntityStatus
{
    [Header("Target")]
    [SerializeField] PlayerEntity playerEntity;

    [Header("Viewable Boss Data")]
    [SerializeField] float HPValue;
    [SerializeField] float MaxHPValue;

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

    private HealthSystem _healthSystem;

    public StanceState EntityStanceState { get; set; }
    public OffensiveState EntityOffensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }
    public Action OnHealthDown { get; set; }
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

    private Alarm alarm = new(DefaultAlarmSize);
    private AttackDefenseSystem _attackDefenseSystem;

    void Start()
    {
        _healthSystem ??= GameManager.GetSystem<HealthSystem>();
        _attackDefenseSystem ??= GameManager.GetSystem<AttackDefenseSystem>();

        _healthSystem.AddNewEntry(nameof(BossEntity), this);

        _healthSystem.SetHealth(nameof(BossEntity), BossStartingMaxHealth);
        _healthSystem.SetMaxHealth(nameof(BossEntity), BossStartingMaxHealth);

        // Test
        _attackDefenseSystem.onParrySuccess = EventManager.AddEvent(101, "GotParriedEvent", () =>
        {
            Debug.Log("You parried Inugami Koko successfully. Stunned for 5 Seconds");
            EntityStanceState = StanceState.Stunned;
            alarm.SetFor(5, Two, true, () => {
                EntityStanceState = StanceState.Idle;
                Debug.Log("Watch out! Inugami Koko is out of stunned state.");
            });
        });

        // We'll now randomize 
        alarm.SetFor(Random.Range(1, 3), Zero, false, () =>
        {
            if (EntityStanceState == StanceState.Stunned) return;
            Debug.Log("Get Ready!!!");
            StartCoroutine(DoNormalAttack());
            alarm[Zero].SetDuration = Random.Range(1, 3);
            alarm[Zero].CurrentTime = Zero;
        });
    }

    IEnumerator DoNormalAttack()
    {
        EntityStanceState = StanceState.Offensive;

        alarm.SetFor(Random.Range(0.25f, 1f), One, true, () =>
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
            motionPercentage = alarm[One].CurrentTime / alarm[One].SetDuration;
            parryPercentageRange = (0.49f, 0.91f);
            if(WasParryTimed)
            {
                Debug.Log("Parry Now!!!");
            }
            yield return new WaitForSeconds(MaxTime);
        }
    }


    void SendDamageToPlayer(float damageValue)
    {
        if (playerEntity.EntityDefensiveState == DefensiveState.Guard)
        {
            _attackDefenseSystem.LosePoise(damageValue);
            damageValue /= 10;
        }
        _healthSystem.SetHealth(nameof(PlayerEntity), -damageValue, isRelative: true);
    }
}