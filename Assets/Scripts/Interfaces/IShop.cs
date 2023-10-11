#nullable enable

using UnityEngine;

internal interface IShop
{
    int ShopValue { get; }
    Sprite? ShopImage { get; }
}