#nullable enable

using Extensions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XVNML2U.Mono;

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
    private CurtainController _curtainController;

    [SerializeField]
    private ObjectPooler itemEntryPool;

    [Header("Dialogue-Specific Information")]
    [SerializeField]
    private string _characterName;
    [SerializeField]
    private XVNMLDialogueControl _dialogueControl;

    private ItemEntry[] itemEntries;

    private const string _dialogueGroupTarget = "CharacterDialogue";

    private void Start()
    {
        _curtainController.Open(null);

        itemEntries ??= itemEntryPool.GetPooledObjects()
            .Select(po => po.GetComponent<ItemEntry>())
            .ToArray();

        ItemEntryModel[]? content = type switch
        {
            ShopType.Item => ItemSystem.ItemList.Select(item => new ItemEntryModel(
                item.ItemName,
                item.ShopValue,
                item.ShopImage
                )).ToArray(),

            ShopType.Skill => SkillSystem.SkillsList.Select(item => new ItemEntryModel(
                item.SkillName,
                item.ShopValue,
                item.ShopImage
                )).ToArray(),

            ShopType.Mado => MadoSystem.MadoList.Select(item => new ItemEntryModel(
                item.MadoName,
                item.ShopValue,
                item.ShopImage
                )).ToArray(),

            ShopType.Deity => DeitySystem.DeityList.Select(item => new ItemEntryModel(
                item.DeityName,
                item.ShopValue,
                item.ShopImage
                )).ToArray(),

            _ => null
        };

        ShowItemEntries(content!);

        if (string.IsNullOrEmpty(_characterName)) return;
        if (string.IsNullOrEmpty(_dialogueGroupTarget)) return;

        _dialogueControl.Play(_characterName, _dialogueGroupTarget);
    }

    public void ShowItemEntries(ItemEntryModel[] entries)
    {
        int i = 0;

        while (i < entries.Length)
        {
            if (itemEntries[i].gameObject.activeInHierarchy == false)
                itemEntries[i].gameObject.Enable();

            itemEntries[i].SetEntry(entries[i]);
            itemEntries[i].targetDialogueControl = _dialogueControl;
            i.Next();
        }

        while (i < itemEntries.Length)
        {
            itemEntries[i].gameObject.Disable();
            i.Next();
        }
    }

    public void ReturnToPlaza()
    {
        _curtainController.Close(() =>
        {
            // Go back to plaza;
            HeavensPlazaSystem.ExitShop();
        });
    }
}