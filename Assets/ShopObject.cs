#nullable enable

using Extensions;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public sealed class ShopObject : MonoBehaviour
{
    enum ShopType
    {
        Item,
        Skill,
        Mado,
        Deity
    }

    [SerializeField]
    private ShopType type;

    [SerializeField]
    private Image shopKeeper;

    [SerializeField]
    private ObjectPooler itemEntryPool;

    private ItemEntry[] itemEntries;


    private void Start()
    {
        itemEntries ??= itemEntryPool.GetPooledObjects()
            .Select(po => po.GetComponent<ItemEntry>())
            .ToArray();

        var content = type switch
        {
            ShopType.Item => ItemSystem.ItemList.Select(item => new ItemEntryModel(
                item.ItemName,
                item.ItemValue,
                item.ItemImage
                )).ToArray(),
            ShopType.Skill => throw new NotImplementedException(),
            ShopType.Mado => throw new NotImplementedException(),
            ShopType.Deity => throw new NotImplementedException(),
            _ => throw new NotImplementedException()
        };

        ShowItemEntries(content);
    }

    public void ShowItemEntries(ItemEntryModel[] entries)
    {
        int i = 0;

        while (i < entries.Length)
        {
            if (itemEntries[i].gameObject.activeInHierarchy == false)
                itemEntries[i].gameObject.Enable();

            itemEntries[i].SetEntry(entries[i]);
            i.Next();
        }

        while (i < itemEntries.Length)
        {
            itemEntries[i].gameObject.Disable();
            i.Next();
        }
    }
}