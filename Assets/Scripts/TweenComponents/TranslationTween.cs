using UnityEngine;

using DG.Tweening;

public class TranslationTween : MonoBehaviour
{
    public enum TranslationAxis
    {
        X, Y, Z
    }

    public TranslationAxis axis;
    public float from, to;

    [Header("Timing")]
    public float duration;
    public float delay;

    public bool doOnAwake = true;

    public void DoTranslationTweeningTo()
    {
        if (delay != 0)
        {
            var delayAlarm = new Alarm(1, floatingPoint: true);
            delayAlarm.SetFor(delay, 0, true, () => DoTranslationTweening(false));
            return;
        }
        DoTranslationTweening(false);
    }

    public void DoTranslationTweeningReturn()
    {
        if(delay != 0)
        {
            var delayAlarm = new Alarm(1, floatingPoint: true);
            delayAlarm.SetFor(delay, 0, true, () => DoTranslationTweening(true));
            return;
        }
        DoTranslationTweening(true);
    }

    private void Awake()
    {
        if (doOnAwake == false) return;
        DoTranslationTweeningTo();
    }

    private void DoTranslationTweening(bool reverse = false)
    {
        var target = reverse ? from : to;
        switch (axis)
        {
            case TranslationAxis.X:
                transform.DOLocalMoveX(target, duration);
                break;
            case TranslationAxis.Y:
                transform.DOLocalMoveY(target, duration);
                break;
            case TranslationAxis.Z:
                transform.DOLocalMoveZ(target, duration);
                break;
        }
    }
}
