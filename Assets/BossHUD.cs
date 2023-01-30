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

    private HealthSystem _healthSystem;
    
    private float smoothHPVelocity;
    
    private string MetricFormat = "{0}/{1}";
    
    private const float SmoothTime = 0.1f;
    private const float MaxTime = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        _healthSystem = GameManager.Command.GetSystem<HealthSystem>();

        StartCoroutine(HPSmoothDampCycle());
    }

    IEnumerator HPSmoothDampCycle()
    {
        yield return DelayedCycle(() =>
        {
            if (_healthSystem == null)
            {
                Debug.Log("So help me child");
                return;
            }
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
                continue;
            }
        }
    }

    void UpdateHPMetrics(string tag)
    {
        if (_healthSystem.Exists(tag) == false) return;

        HPSlider.maxValue = _healthSystem[tag].MaxHealthValue;
        HPSlider.minValue = 0;
        HPSlider.value = Mathf.SmoothDamp(HPSlider.value, _healthSystem[tag].HealthValue, ref smoothHPVelocity, SmoothTime);

        HPMetrics.text = string.Format(MetricFormat, _healthSystem[tag].HealthValue, _healthSystem[tag].MaxHealthValue);
    }
}
