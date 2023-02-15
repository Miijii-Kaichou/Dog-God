#nullable enable

using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class FadeInTween : MonoBehaviour
{
    [SerializeField, Header("Graphics")]
    private Graphic _targetGraphic;

    [Header("Configurations")]
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private bool _playOnAwake = true;
    internal int delay = 0;

    public Action? OnEffectComplete { get; internal set; }

    private void Awake()
    {
        if (_playOnAwake == false) return;
        if(delay != 0)
        {
            var alarm = new Alarm(1);
            alarm.SetFor(delay, 0, true, DoFadeInTweening);
            return;
        }
        DoFadeInTweening();
    }

    public void DoFadeInTweening()
    {
        _targetGraphic.
            DOFade(1, _duration).
            OnComplete(() => OnEffectComplete?.Invoke());
    }
}