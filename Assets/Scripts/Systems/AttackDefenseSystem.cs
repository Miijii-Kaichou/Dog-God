using System;
using System.Collections;
using UnityEngine;

using static SharedData.Constants;

public class AttackDefenseSystem : GameSystem
{
    private static AttackDefenseSystem Self => (AttackDefenseSystem)Instance;

    public bool AreInputsReversed
    {
        get
        {
            return ReversalValue == 1;
        }
        set
        {
            ReversalValue = value ? 1 : 0;
        }
    }

    private static float LeftClickHoldFrames = 0;
    private static float RightClickHoldFrames = 0;

    private static float PoiseValue = 100;
    private static float GuardStress = 1;
    
    private static int ReversalValue = 0;

    private const int LeftClick = 0;
    private const int RightClick = 1;
    private const float GuardTimeThreshold = 0.1f;
    private const float ChargeAttackTimeThreshold = 0.5f;

    public static EventCall OnAttack;
    public static EventCall OnChargedAttack;
    public static EventCall OnGuard;
    public static EventCall OnParry;
    public static EventCall OnParrySuccess;
    public static EventCall OnPoiseLost;

    static ILevelProperty PlayerLevel => Self.Player;
    static IEntityStatus PlayerStanceState => Self.Player;

    protected override void OnInit()
    {
        StartCoroutine(GuardStressCycle());
    }

    protected override void Main()
    {
        ListenForAttackInput();
        ListenForDefenseInput();
        CheckAndUpdateStanceStatusIfNoInput();
    }

    static IEnumerator GuardStressCycle()
    {
        while(true)
        {
            ManageGuardStressLevels();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private static void ManageGuardStressLevels()
    {
        GuardStress -= GuardStress > One ? One : Zero;
    }

    private static void CheckAndUpdateStanceStatusIfNoInput()
    {
        if (Input.GetMouseButton(LeftClick + ReversalValue) || Input.GetMouseButton(RightClick - ReversalValue)) return;
        PlayerStanceState.ChangeStanceState(StanceState.Idle);
    }

    #region Attack Input+Logic
    private void ListenForAttackInput()
    {
        if (Self.Player == null) return;

        if (Input.GetMouseButton(LeftClick + ReversalValue))
        {
            BeInOffensiveStance();
        }

        if (Input.GetMouseButtonUp(LeftClick + ReversalValue))
        {
            ExecuteAttack();
            LeftClickHoldFrames = Zero;
            PlayerStanceState.ChangeOffensiveState(OffensiveState.None);
        }
    }

    private void BeInOffensiveStance()
    {
        PlayerStanceState.ChangeStanceState(StanceState.Offensive);
        LeftClickHoldFrames += Time.deltaTime;
    }

    private static void ExecuteAttack()
    {
        PlayerLevel.AddExperience();

        HealthSystem.SetHealth(BossEntityTag, -952, isRelative: true);

        if (LeftClickHoldFrames > ChargeAttackTimeThreshold)
        {
            PlayerStanceState.ChangeOffensiveState(OffensiveState.ChargeAttack);
            OnChargedAttack.Trigger();
            return;
        }

        PlayerStanceState.ChangeOffensiveState(OffensiveState.Attack);
        OnAttack.Trigger();
    }
    #endregion

    #region Defense Input+Logic
    private static void ListenForDefenseInput()
    {
        if (Self.Player == null) return;

        if (Input.GetMouseButton(RightClick - ReversalValue))
        {
            BeInDefensiveStance();
        }

        if (Input.GetMouseButtonUp(RightClick - ReversalValue))
        {
            ExecuteDefense();
            RightClickHoldFrames = Zero;
            PlayerStanceState.ChangeDefensiveState(DefensiveState.None);
        }
    }

    private static void BeInDefensiveStance()
    {
        PlayerStanceState.ChangeStanceState(StanceState.Defensive);
        RightClickHoldFrames += Time.deltaTime;
        if (RightClickHoldFrames > GuardTimeThreshold)
        {
            PlayerStanceState.ChangeDefensiveState(DefensiveState.Guard);
            OnGuard.Trigger();
            return;
        }
    }

    private static void ExecuteDefense()
    {
        if (RightClickHoldFrames < GuardTimeThreshold) PlayerStanceState.ChangeDefensiveState(DefensiveState.Parry);
        OnParry.Trigger();
    }

    internal static void LosePoise(float value)
    {
        PoiseValue -= value * GuardStress;
        PoiseValue = Mathf.Clamp(PoiseValue, Zero, MaxGuardStress);
        AmplifyStress();
        if (PoiseValue == Zero) OnPoiseLost.Trigger();
    }

    private static void AmplifyStress()
    {
        GuardStress += GuardStress * Two;
    }

    internal static void RestorePoise()
    {
        PoiseValue = MaxGuardStress;
    }
    #endregion
}
