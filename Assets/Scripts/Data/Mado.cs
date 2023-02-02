using System;
using UnityEngine;
using static SharedData.Constants;

#nullable enable

public abstract class Mado : IActionableItem
{
    public short? MadoID { get; private set; }

    public virtual string? MadoName { get; }
    public virtual Type? StaticItemType { get; }
    public virtual ItemUseCallaback? OnActionUse { get; }
    public virtual int MadoEnhancementValue { get; }
    
    public int SlotNumber { get; set; }

    public Mado()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            var madoSystem = GameManager.GetSystem<MadoSystem>();
            MadoID = (short)madoSystem.GetRefCount();
            Debug.Log($"MadoID ({MadoID}) {{{MadoName}}}");
            madoSystem.IncreaseRefCount();
        };
    }

    public void UseAction()
    {
        OnActionUse?.Invoke();
    }
}