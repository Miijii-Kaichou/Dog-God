#nullable enable

using DG.Tweening;
using System;
using UnityEngine;

public class CanvasGroupFadeInTween : MonoBehaviour
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
        DoFadeInTweening();
    }

    public void DoFadeInTweening()
    {
        _targetGroup.
            DOFade(1, _duration).
            OnComplete(() => OnEffectComplete?.Invoke());
    }
}