using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static SharedData.Constants;

public class BossHUD : MonoBehaviour
{
    [Header("Boss Health UI")]
    [SerializeField] private Slider HPSlider;
    [SerializeField] private TextMeshProUGUI HPMetrics;

    private float smoothHPVelocity;

    private string MetricFormat = "{0}/{1}";

    private const float SmoothTime = 0.1f;
    private const float MaxTime = 0.01f;

    private void Start()
    {
        StartCoroutine(HPSmoothDampCycle());
    }

    IEnumerator HPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            UpdateHPMetrics(BossEntityTag);
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

    void UpdateHPMetrics(string tag)
    {
        if (HealthSystem.Self == null) return;
        if (HealthSystem.Exists(tag) == false) return;

        HPSlider.maxValue = HealthSystem.Self[tag].MaxHealthValue;
        HPSlider.minValue = 0;
        HPSlider.value = Mathf.SmoothDamp(HPSlider.value, HealthSystem.Self[tag].HealthValue, ref smoothHPVelocity, SmoothTime);

        HPMetrics.text = string.Format(MetricFormat, HealthSystem.Self[tag].HealthValue, HealthSystem.Self[tag].MaxHealthValue);
    }
}
