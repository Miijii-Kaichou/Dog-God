#nullable enable

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI? itemNameText;
    [SerializeField] private Image? itemImage;
    [SerializeField] private TextMeshProUGUI? itemPriceText;

    public void SetEntry(string itemName, string itemPrice, Sprite? texture)
    { 
        itemNameText!.text = itemName; ;
        itemPriceText!.text = itemPrice;
        itemImage!.sprite = texture;
    }
}
