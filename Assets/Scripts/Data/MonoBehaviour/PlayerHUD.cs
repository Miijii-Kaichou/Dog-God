using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static SharedData.Constants;

public class PlayerHUD : MonoBehaviour
{
    PlayerEntity Player;

    [Header("Player Health UI")]
    [SerializeField] private Slider HPSlider;
    [SerializeField] private TextMeshProUGUI HPMetrics;

    [Header("Player Mana UI")]
    [SerializeField] private Slider MPSlider;
    [SerializeField] private TextMeshProUGUI MPMetrics;

    [Header("Player Experience UI")]
    [SerializeField] private Slider EXPSlider;
    [SerializeField] private TextMeshProUGUI LevelMetrics;
    [SerializeField] private TextMeshProUGUI EXPMetrics;

    [Header("Player Action Points")]
    [SerializeField] private TextMeshProUGUI ActionPointsInfo;

    [Header("Player Action System UI (Primary Selection)")]
    [SerializeField] private ActionCategoryUi PlayerActionSystemCategoryUI;
    [SerializeField] private Color actionInactiveStateColor;
    [SerializeField] private Color actionActiveStateColor;

    [Header("Player Action System UI (Secondary Selection")]
    [SerializeField] private ActionItemUI PlayerActionSystemItemUI;

    private string MetricFormat = "{0}/{1}";
    private string LevelFormat = "LV {0}";
    private float targetAngle;
    private float zEulerAngleVelocity;
    private float smoothHPVelocity;
    private float smoothMPVelocity;
    private float smoothEXPVelocity;
    private const float MaxDegrees = 360f;
    private const float RotationUnit = 90f;

    private void Awake()
    {
        GameManager.OnPlayerRegistered = () => {  
            Player = GameManager.Player; 
        };
    }

    private void Start()
    {
        ExperienceSystem.onLevelChange = () =>
        {
            EXPSlider.value = EXPSlider.minValue;
        };

        ActionSystem.RegisterPlayerHUD(this);

        StartCoroutine(HPSmoothDampCycle());
        StartCoroutine(MPSmoothDampCycle());
        StartCoroutine(EXPSmoothDampCycle());

        StartCoroutine(ActionColorFade());
        StartCoroutine(ActionViewRotation());
        StartCoroutine(ItemAlphaFade());
    }

    #region Coroutines
    IEnumerator HPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            UpdateHPMetrics(PlayerEntityTag);
        });
    }

    IEnumerator MPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            UpdateManaMetrics();
        });
    }

    IEnumerator EXPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            UpdateExperienceMetrics();
        });
    }

    IEnumerator ActionColorFade()
    {
        yield return DelayedCycle(() =>
        {
            if (ActionSystem.IsPerformingAction)
            {
                PlayerActionSystemCategoryUI.DOColor(actionActiveStateColor, SmoothTime);
                return;
            }

            PlayerActionSystemCategoryUI.DOColor(actionInactiveStateColor, SmoothTime);
        });
    }

    IEnumerator ItemAlphaFade()
    {
        yield return DelayedCycle(() =>
        {
            if (ActionSystem.IsCategorySelected)
            {
                PlayerActionSystemItemUI.Reveal();
                return;
            }

            PlayerActionSystemItemUI.Hide();
        });
    }

    IEnumerator ActionViewRotation()
    {
        yield return DelayedCycle(() =>
        {
            float zEulerAngle = Mathf.SmoothDamp(PlayerActionSystemCategoryUI.transform.localEulerAngles.z, targetAngle, ref zEulerAngleVelocity, SmoothTime);
            PlayerActionSystemCategoryUI.transform.localEulerAngles = new Vector3(Zero, Zero, zEulerAngle);
        });
    }

    IEnumerator DelayedCycle(Action function, float timeInSecs = MaxTime)
    {
        float elapsedTime = Zero;

        while (true)
        {
            function();
            elapsedTime += Time.deltaTime;
            float timeTilUpdate = timeInSecs - elapsedTime;

            if (timeTilUpdate <= Zero)
            {
                yield return null;
                elapsedTime = Zero;
            }
        }
    }
    #endregion

    #region Ui Update Methods
    void UpdateHPMetrics(string tag)
    {
        if (HealthSystem.Self == null) return;
        if (HealthSystem.Exists(tag) == false) return;

        HPSlider.maxValue = HealthSystem.Self[tag].MaxHealthValue;
        HPSlider.minValue = 0;
        HPSlider.value = Mathf.SmoothDamp(HPSlider.value, HealthSystem.Self[tag].HealthValue, ref smoothHPVelocity, SmoothTime);

        HPMetrics.text = string.Format(MetricFormat, HealthSystem.Self[tag].HealthValue, HealthSystem.Self[tag].MaxHealthValue);
    }
    void UpdateManaMetrics()
    {
        if (ManaSystem.Self == null) return;
        MPSlider.maxValue = Player.MaxManaValue;
        MPSlider.minValue = 0;
        MPSlider.value = Mathf.SmoothDamp(MPSlider.value, Player.ManaValue, ref smoothMPVelocity, SmoothTime);

        MPMetrics.text = string.Format(MetricFormat, Player.ManaValue, Player.ManaValue);
    }
    void UpdateExperienceMetrics()
    {
        if (ExperienceSystem.Self == null) return;

        EXPSlider.maxValue = 1;
        EXPSlider.minValue = 0;

        EXPSlider.value = Mathf.SmoothDamp(EXPSlider.value, ExperienceSystem.CurrentExperience % 1, ref smoothEXPVelocity, SmoothTime);

        EXPMetrics.text = string.Format(MetricFormat, Mathf.RoundToInt(ExperienceSystem.CurrentExperiencePoints), Mathf.RoundToInt(ExperienceSystem.ExperienceTilNextLevel));
        LevelMetrics.text = string.Format(LevelFormat, ExperienceSystem.CurrentLevel);
    }
    #endregion

    #region Player Action UI Methods
    public void UpdateTargetAngle(int units)
    {
        targetAngle += RotationUnit * units % MaxDegrees;
    }

    public void UpdateSlotIndex(int index)
    {
        PlayerActionSystemCategoryUI.EnlargeSlot(index);
    }

    internal void ResetActionUi()
    {
        targetAngle = Zero;
        PlayerActionSystemCategoryUI.NormalizeAllSlots();
    }
    #endregion
}