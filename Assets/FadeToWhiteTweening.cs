using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XVNML.Core.Native;

public class FadeToWhiteTweening : MonoBehaviour
{
    [SerializeField]
    CanvasGroupFadeInTween _fadeToWhiteTween;

    private void Start()
    {
        if (_fadeToWhiteTween == null) return;
        RuntimeReferenceTable.Set("FadeToWhiteTween", _fadeToWhiteTween);
    }
}
