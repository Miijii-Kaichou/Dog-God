using System;
using System.Collections;
using UnityEngine;

using static SharedData.Constants;

public class AttackDefenseSystem : GameSystem, IRegisterEntity<PlayerEntity>
{
    public bool AreInputsReversed
    {
        get
        {
            return reversalValue == 1;
        }
        set
        {
            reversalValue = value ? 1 : 0;
        }
    }

    public PlayerEntity EntityReference { get; set; }

    private float leftClickHoldFrames = 0;
    private float rightClickHoldFrames = 0;

    [SerializeField] private float poise = 100;
    [SerializeField] private float guardStress = 1;
    
    private int reversalValue = 0;

    private const int LeftClick = 0;
    private const int RightClick = 1;
    private const float GuardTimeThreshold = 0.1f;
    private const float ChargeAttackTimeThreshold = 0.5f;

    HealthSystem healthSystem;

    public EventCall onGuard;
    public EventCall onParry;
    public EventCall onAttack;
    public EventCall onChargedAttack;
    public EventCall onParrySuccess;
    public EventCall onPoiseLost;

    protected override void OnInit()
    {
        healthSystem ??= GameManager.GetSystem<HealthSystem>();
        StartCoroutine(GuardStressCycle());
    }

    protected override void Main()
    {
        ListenForAttackInput();
        ListenForDefenseInput();
        CheckAndUpdateStanceStatusIfNoInput();
    }

    IEnumerator GuardStressCycle()
    {
        while(true)
        {
            ManageGuardStressLevels();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void ManageGuardStressLevels()
    {
        guardStress -= guardStress > One ? One : Zero;
    }

    private void CheckAndUpdateStanceStatusIfNoInput()
    {
        if (Input.GetMouseButton(LeftClick + reversalValue) || Input.GetMouseButton(RightClick - reversalValue)) return;
        ((IEntityStatus)EntityReference).ChangeStanceState(StanceState.Idle);
    }

    #region Attack Input+Logic
    private void ListenForAttackInput()
    {
        if (EntityReference == null) return;

        if (Input.GetMouseButton(LeftClick + reversalValue))
        {
            BeInOffensiveStance();
        }

        if (Input.GetMouseButtonUp(LeftClick + reversalValue))
        {
            ExecuteAttack();
            leftClickHoldFrames = Zero;
            ((IEntityStatus)EntityReference).ChangeOffensiveState(OffensiveState.None);
        }
    }

    private void BeInOffensiveStance()
    {
        ((IEntityStatus)EntityReference).ChangeStanceState(StanceState.Offensive);
        leftClickHoldFrames += Time.deltaTime;
    }

    private void ExecuteAttack()
    {
        EntityReference.AddExperience();

        healthSystem.SetHealth(BossEntityTag, -952, isRelative: true);

        if (leftClickHoldFrames > ChargeAttackTimeThreshold)
        {
            ((IEntityStatus)EntityReference).ChangeOffensiveState(OffensiveState.ChargeAttack);
            onChargedAttack.Trigger();
            return;
        }

        ((IEntityStatus)EntityReference).ChangeOffensiveState(OffensiveState.Attack);
        onAttack.Trigger();
    }
    #endregion

    #region Defense Input+Logic
    private void ListenForDefenseInput()
    {
        if (EntityReference == null) return;

        if (Input.GetMouseButton(RightClick - reversalValue))
        {
            BeInDefensiveStance();
        }

        if (Input.GetMouseButtonUp(RightClick - reversalValue))
        {
            ExecuteDefense();
            rightClickHoldFrames = Zero;
            ((IEntityStatus)EntityReference).ChangeDefensiveState(DefensiveState.None);
        }
    }

    private void BeInDefensiveStance()
    {
        ((IEntityStatus)EntityReference).ChangeStanceState(StanceState.Defensive);
        rightClickHoldFrames += Time.deltaTime;
        if (rightClickHoldFrames > GuardTimeThreshold)
        {
            ((IEntityStatus)EntityReference).ChangeDefensiveState(DefensiveState.Guard);
            onGuard.Trigger();
            return;
        }
    }

    private void ExecuteDefense()
    {
        if (rightClickHoldFrames < GuardTimeThreshold) ((IEntityStatus)EntityReference).ChangeDefensiveState(DefensiveState.Parry);
        onParry.Trigger();
    }

    internal void LosePoise(float value)
    {
        poise -= value * guardStress;
        poise = Mathf.Clamp(poise, Zero, MaxGuardStress);
        AmplifyStress();
        if (poise == Zero) onPoiseLost.Trigger();
    }

    void AmplifyStress()
    {
        guardStress += guardStress * Two;
    }

    internal void RestorePoise()
    {
        poise = MaxGuardStress;
    }
    #endregion
}
