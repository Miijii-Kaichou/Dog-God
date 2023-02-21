#nullable enable

using System;
using UnityEngine;

[Serializable]
public struct ItemEntryModel
{
    public string? itemName;
    public int? itemPrice;
    public Sprite? texture;

    public ItemEntryModel(string? itemName, int? itemPrice, Sprite? texture)
    {
        this.itemName = itemName;
        this.itemPrice = itemPrice;
        this.texture = texture;
    }
}