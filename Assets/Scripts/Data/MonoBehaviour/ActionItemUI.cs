using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ActionItemUI : MonoBehaviour
{

    [SerializeField] private ActionItemSlot[] actionItemSlots = new ActionItemSlot[4];
    private CanvasGroup canvasGroup;

    private int previousSlot, currentSlot;
    private const float SelectedScale = 2;

    private void Start()
    {
        actionItemSlots ??= new ActionItemSlot[4];
        actionItemSlots = GetComponentsInChildren<ActionItemSlot>();
        canvasGroup = canvasGroup != null ? canvasGroup : GetComponent<CanvasGroup>();
    }

    public void Reveal()
    {
        canvasGroup.DOFade(1, 0.2f);
    }

    public void Hide()
    {
        canvasGroup.DOFade(0, 0.2f);
    }
}