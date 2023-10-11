#nullable enable

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class CurtainController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _curtainCanvasGroup;

    [Header("Events")]
    [SerializeField] private UnityEvent _onFadeIn;
    [SerializeField] private UnityEvent _onFadeOut;

    public void Open(TweenCallback? action)
    {
        // Make sure we can't click on anything right now.
        _curtainCanvasGroup.blocksRaycasts = true;

        _onFadeIn?.Invoke();

        // Fade in to view.
        var animation = _curtainCanvasGroup
            .DOFade(0, 1)
            .OnComplete(() =>
            {
                action?.Invoke();
                _curtainCanvasGroup.blocksRaycasts = false;
            });
    }

    public void Close(TweenCallback? action)
    {
        // Make sure we can't click on anything anymore.
        _curtainCanvasGroup.blocksRaycasts = true;

        _onFadeOut?.Invoke();

        // Fade Out To Black
        _curtainCanvasGroup
            .DOFade(1, 1)
            .OnComplete(() =>
            {
                action?.Invoke();
                _curtainCanvasGroup.blocksRaycasts = false;
            });
    }
}
