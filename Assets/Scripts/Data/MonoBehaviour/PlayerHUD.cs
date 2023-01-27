using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static SharedData.Constants;

public class PlayerHUD : MonoBehaviour
{
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

    // Dependencies
    private HealthSystem _healthSystem;
    private ManaSystem _manaSystem;
    private LevelingSystem _levelingSystem;
    private ActionSystem _actionSystem;

    private string MetricFormat = "{0}/{1}";
    private string LevelFormat = "LV {0}";
    private float targetAngle;
    private float zEulerAngleVelocity;
    private float smoothHPVelocity;
    private float smoothMPVelocity;
    private float smoothEXPVelocity;
    private const float SmoothTime = 0.1f;
    private const float MaxDegrees = 360f;
    private const float MaxTime = 0.01f;
    private const float RotationUnit = 90f;

    private void Start()
    {
        _healthSystem = GameManager.Command.GetSystem<HealthSystem>();
        _manaSystem = GameManager.Command.GetSystem<ManaSystem>();
        _levelingSystem = GameManager.Command.GetSystem<LevelingSystem>();
        _actionSystem = GameManager.Command.GetSystem<ActionSystem>();

        _levelingSystem.onLevelChange = () =>
        {
            EXPSlider.value = EXPSlider.minValue;
        };

        _actionSystem.RegisterPlayerHUD(this);

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
            if (_healthSystem == null) return;
            UpdateHPMetrics(PlayerEntityTag);
        });
    }

    IEnumerator MPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            if (_manaSystem == null) return;
            UpdateManaMetrics();
        });
    }

    IEnumerator EXPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            if (_levelingSystem == null) return;
            UpdateExperienceMetrics();
        });
    }

    IEnumerator ActionColorFade()
    {
        yield return DelayedCycle(() =>
        {
            if (_actionSystem.IsPerformingAction)
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
            if (_actionSystem.IsCategorySelected)
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
                continue;
            }
        }
    }
    #endregion

    #region Ui Update Methods
    void UpdateHPMetrics(string tag)
    {
        if (_healthSystem.Exists(tag) == false) return;

        HPSlider.maxValue = _healthSystem[tag].MaxHealthValue;
        HPSlider.minValue = 0;
        HPSlider.value = Mathf.SmoothDamp(HPSlider.value, _healthSystem[tag].HealthValue, ref smoothHPVelocity, SmoothTime);

        HPMetrics.text = string.Format(MetricFormat, _healthSystem[tag].HealthValue, _healthSystem[tag].MaxHealthValue);
    }
    void UpdateManaMetrics()
    {
        if (_manaSystem.EntityRef == null) return;

        MPSlider.maxValue = _manaSystem.EntityRef.MaxManaValue;
        MPSlider.minValue = 0;
        MPSlider.value = Mathf.SmoothDamp(MPSlider.value, _manaSystem.EntityRef.ManaValue, ref smoothMPVelocity, SmoothTime);

        MPMetrics.text = string.Format(MetricFormat, _manaSystem.EntityRef.ManaValue, _manaSystem.EntityRef.ManaValue);
    }
    void UpdateExperienceMetrics()
    {
        if (_levelingSystem.EntityRef == null) return;

        EXPSlider.maxValue = 1;
        EXPSlider.minValue = 0;

        EXPSlider.value = Mathf.SmoothDamp(EXPSlider.value, _levelingSystem.CurrentExperience % 1, ref smoothEXPVelocity, SmoothTime);

        EXPMetrics.text = string.Format(MetricFormat, Mathf.RoundToInt(_levelingSystem.CurrentExperiencePoints), Mathf.RoundToInt(_levelingSystem.ExperienceTilNextLevel));
        LevelMetrics.text = string.Format(LevelFormat, _levelingSystem.CurrentLevel);
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