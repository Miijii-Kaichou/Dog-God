using TMPro;
using UnityEngine;
using XVNML2U.Mono;

using Extensions;

public sealed class DialogueControlModifier : MonoBehaviour
{
    [SerializeField]
    XVNMLDialogueControl _xvnmlDialogueControl;

    public void SetCastNameTextComponent(XVNMLTextRenderer component)
    {
        _xvnmlDialogueControl
            .FindProperty("_castNameText")
            .UpdateWith(component);
    }

    public void SetMainTextRenderer(XVNMLTextRenderer component)
    {
        _xvnmlDialogueControl
            .FindProperty("_mainTextr")
            .UpdateWith(component);
    }

    public void SetConfirmMarkerGraphic(ConfirmMarker component)
    {
        _xvnmlDialogueControl
            .FindProperty("_confirmMarker")
            .UpdateWith(component);
    }
}
