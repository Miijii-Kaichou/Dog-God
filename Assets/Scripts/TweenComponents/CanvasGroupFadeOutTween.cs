#nullable enable

using DG.Tweening;
using System;
using UnityEngine;

public sealed class CanvasGroupFadeOutTween : MonoBehaviour
{
    [SerializeField, Header("Graphics")]
    private CanvasGroup _targetGroup;

    [Header("Configurations")]
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private bool _playOnAwake = true;

    public Action? OnEffectComplete { get; internal set; }

    private void Awake()
    {
        if (_playOnAwake == false) return;
        DoFadeOutTweening();
    }

    public void DoFadeOutTweening()
    {
        _targetGroup.
            DOFade(0, _duration).
            OnComplete(() => OnEffectComplete?.Invoke());
    }
}