using System;
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

    public StanceState EntityStanceState { get; set; }
    public OffensiveState EntityOffsensiveState { get; set; }
    public DefensiveState EntityDefensiveState { get; set; }

    void Start()
    {
        ((IHealthProperty)this).SetMaxHealth(100000);
    }
}