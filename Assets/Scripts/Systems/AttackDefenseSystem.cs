using System;
using UnityEngine;

public class AttackDefenseSystem : GameSystem, IRegisterPlayer<PlayerEntity>
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

    public PlayerEntity EntityRef { get; set; }

    private float leftClickHoldFrames = 0;
    private float rightClickHoldFrames = 0;

    private int reversalValue = 0;

    private const int LeftClick = 0;
    private const int RightClick = 1;
    private const float GuardTimeThreshold = 0.1f;
    private const float ChargeAttackTimeThreshold = 0.5f;

    protected override void OnInit()
    {

    }

    protected override void Main()
    {
        ListenForAttackInput();
        ListenForDefenseInput();
        CheckAndUpdateStanceStatusIfNoInput();
    }

    private void CheckAndUpdateStanceStatusIfNoInput()
    {
        if (Input.GetMouseButton(LeftClick + reversalValue) || Input.GetMouseButton(RightClick - reversalValue)) return;
        ((IEntityStatus)EntityRef).ChangeStanceState(StanceState.Idle);
    }

    #region Attack Input+Logic
    private void ListenForAttackInput()
    {
        if (EntityRef == null) return;

        if (Input.GetMouseButton(LeftClick + reversalValue))
        {
            BeInOffensiveStance();
        }

        if (Input.GetMouseButtonUp(LeftClick + reversalValue))
        {
            ExecuteAttack();
            leftClickHoldFrames = 0;
            ((IEntityStatus)EntityRef).ChangeOffensiveState(OffensiveState.None);
        }
    }

    private void BeInOffensiveStance()
    {
        ((IEntityStatus)EntityRef).ChangeStanceState(StanceState.Offensive);
        leftClickHoldFrames += Time.deltaTime;
    }

    private void ExecuteAttack()
    {
        EntityRef.AddExperience();

        if (leftClickHoldFrames > ChargeAttackTimeThreshold)
        {
            ((IEntityStatus)EntityRef).ChangeOffensiveState(OffensiveState.ChargeAttack);
            return;
        }

        ((IEntityStatus)EntityRef).ChangeOffensiveState(OffensiveState.Attack);
    }
    #endregion

    #region Defense Input+Logic
    private void ListenForDefenseInput()
    {
        if (EntityRef == null) return;

        if (Input.GetMouseButton(RightClick - reversalValue))
        {
            BeInDefensiveStance();
        }

        if (Input.GetMouseButtonUp(RightClick - reversalValue))
        {
            ExecuteDefense();
            rightClickHoldFrames = 0;
            ((IEntityStatus)EntityRef).ChangeDefensiveState(DefensiveState.None);
        }
    }

    private void BeInDefensiveStance()
    {
        ((IEntityStatus)EntityRef).ChangeStanceState(StanceState.Defensive);
        rightClickHoldFrames += Time.deltaTime;
        if (rightClickHoldFrames > GuardTimeThreshold)
        {
            ((IEntityStatus)EntityRef).ChangeDefensiveState(DefensiveState.Guard);
            return;
        }
    }

    private void ExecuteDefense()
    {
        if (rightClickHoldFrames < GuardTimeThreshold) ((IEntityStatus)EntityRef).ChangeDefensiveState(DefensiveState.Parry);
    } 
    #endregion
}
