using UnityEngine;
using UnityEngine.UI;

public class ActionItemSlot : MonoBehaviour
{
    public Image SlotGraphic { get; private set; }

    private void Start()
    {
        SlotGraphic = GetComponent<Image>();
    }

    public void SelectItem()
    {

    }
}