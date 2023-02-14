using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTween : MonoBehaviour
{
    Transform _transform;

    [SerializeField] private float UpperLimitValue;
    [SerializeField] private float LowerLimitValue;

    // Start is called before the first frame update
    void Start()
    {
        _transform = gameObject.transform;

        Vector3 upperLimitVector = _transform.localPosition + new Vector3(0, UpperLimitValue, 0);
        Vector3 lowerLimitVector = _transform.localPosition - new Vector3(0, LowerLimitValue, 0);
        Vector3 centerVector = _transform.localPosition;

        var tween = _transform.DOLocalPath(new Vector3[] { upperLimitVector, lowerLimitVector}, 3, PathType.Linear);
        tween.SetLoops(-1);
        tween.Play();
    }
}
