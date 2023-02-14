using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ActionCategorySlot : MonoBehaviour
{
    public Image SlotGraphic { get; private set; }
    ActionItemUI _itemSelectionUI;

    // Start is called before the first frame update
    void Start()
    {
        SlotGraphic = GetComponent<Image>();   
        _itemSelectionUI= GetComponentInChildren<ActionItemUI>();
    }

    public void ViewItems()
    {
        _itemSelectionUI.Reveal();
    }

    public void HideItems()
    {
        _itemSelectionUI.Hide();
    }
}
