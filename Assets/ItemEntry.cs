#nullable enable

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI? itemNameText;
    [SerializeField] private Image? itemImage;
    [SerializeField] private TextMeshProUGUI? itemPriceText;
    [SerializeField] private string? itemDescription;

    public void SetEntry(ItemEntryModel model)
    { 
        itemNameText!.text = model.itemName; ;
        itemPriceText!.text = model.itemPrice.ToString();
        //itemImage!.sprite = model.texture;
    }

    void DescribeEntry()
    {
        //TODO: Have the Shopkeeper describe the item
        
    }
}
