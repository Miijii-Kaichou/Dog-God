using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActionCategoryUi : MonoBehaviour
{
    [Header("Image Elements")]
    [SerializeField] Image mainImage;

    [SerializeField]private Image[] categorySlots;

    private int previousSlot, currentSlot; 
    private const float SelectedScale = 2;
    private const float UnselectedScale = 1;
    private const int MaxSlots = 4;

    private void Start()
    {
        categorySlots ??= new Image[MaxSlots];
        categorySlots = GetComponentsInChildren<Image>();
    }

    public void EnlargeSlot(int index)
    {
        previousSlot = currentSlot;
        currentSlot = index;
        NormalizeSlot(previousSlot);
        var targetSlot = categorySlots[index];
        targetSlot.transform.DOScale(SelectedScale, 0.1f);
    }

    void NormalizeSlot(int index)
    {
        var targetSlot = categorySlots[index];
        targetSlot.transform.DOScale(UnselectedScale, 0.1f);
    }

    public void NormalizeAllSlots()
    {
        int index = 0;
        NormalizeSlot(index++);
        NormalizeSlot(index++);
        NormalizeSlot(index++);
        NormalizeSlot(index);
    }

    public void DOColor(Color endColor, float duration)
    {
        // We don't need to use a lot for this one;
        int index = 0;
        mainImage.DOColor(endColor, duration);
        categorySlots[index++].DOColor(endColor, duration);
        categorySlots[index++].DOColor(endColor, duration);
        categorySlots[index++].DOColor(endColor, duration);
        categorySlots[index].DOColor(endColor, duration);
    }
}
