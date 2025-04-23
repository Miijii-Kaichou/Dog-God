#nullable enable

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XVNML2U.Mono;

public class ItemEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI? itemNameText;
    [SerializeField] private Image? itemImage;
    [SerializeField] private TextMeshProUGUI? itemPriceText;
    private Button _itemButton;
    
    
    internal XVNMLDialogueControl targetDialogueControl;
    
    // Dialogue Group Target for description
    const string DialogueGroupTarget = "ItemDescriptions";

    private void Awake()
    {
        _itemButton ??= GetComponent<Button>();
    }

    public void SetEntry(ItemEntryModel model)
    { 
        itemNameText!.text = model.itemName; ;
        itemPriceText!.text = model.itemPrice.ToString();

        if (_itemButton == null) return;
        _itemButton.onClick.AddListener(delegate { ReadDescription(); });
        //itemImage!.sprite = model.texture;
    }

    public void ReadDescription()
    {
        //TODO: Have the Shopkeeper describe the item
        targetDialogueControl.Play(itemNameText?.text, DialogueGroupTarget);
    }
}
