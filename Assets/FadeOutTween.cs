#nullable enable

using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutTween : MonoBehaviour
{
    [SerializeField, Header("Graphics")]
    private Graphic _targetGraphic;

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
        _targetGraphic.
            DOFade(0, _duration).
            OnComplete(() => OnEffectComplete?.Invoke());
    }
}
