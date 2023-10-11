using System;
using UnityEngine;
using static SharedData.Constants;

#nullable enable

public abstract class Mado : IActionableItem, IShop
{
    protected PlayerEntity? Player { get; private set; }
    public short? MadoID { get; private set; }

    public virtual string? MadoName { get; }
    public virtual int ShopValue { get; }
    public virtual Sprite? ShopImage { get; }

    public virtual Type? StaticItemType { get; }
    public virtual ItemUseCallback? OnActionUse { get; }
    public virtual int MadoEnhancementValue { get; }
    
    public int SlotNumber { get; set; }

    public bool EnabledIf => true;

    public Mado()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            Player = GameManager.Player;
            MadoID = (short)MadoSystem.GetRefCount();
            Debug.Log($"MadoID ({MadoID}) {{{MadoName}}}");
            MadoSystem.IncreaseRefCount();
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}