using UnityEngine;

using DG.Tweening;

public class TranslationTween : MonoBehaviour
{
    public enum TranslationAxis
    {
        X, Y, Z
    }

    public TranslationAxis axis;
    public float to;

    [Header("Timing")]
    public float duration;
    public float delay;

    public bool doOnAwake = true;

    public void Activate()
    {
        if (delay != 0)
        {
            var delayAlarm = new Alarm(1, floatingPoint: true);
            delayAlarm.SetFor(delay, 0, true, DoTranslationTweening);
            return;
        }
        DoTranslationTweening();
    }

    private void Awake()
    {
        if (doOnAwake == false) return;
        Activate();
    }

    private void DoTranslationTweening()
    {
        switch (axis)
        {
            case TranslationAxis.X:
                transform.DOLocalMoveX(to, duration);
                break;
            case TranslationAxis.Y:
                transform.DOLocalMoveY(to, duration);
                break;
            case TranslationAxis.Z:
                transform.DOLocalMoveZ(to, duration);
                break;
        }
    }
}
