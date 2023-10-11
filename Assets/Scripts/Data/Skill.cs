#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Skill : IActionableItem, IShop
{
    private Stack<float> EnhancementValueStack = new();
    
    protected PlayerEntity? Player { get; private set; }
    protected BossEntity? Boss { get; private set; }
    protected short? SkillID { get; private set; }
    protected float EnhancementValue => EnhancementValueStack.Count == 0f ? 
        1f :
        EnhancementValueStack.Sum(x => x);

    public virtual string? SkillName { get; }
    public virtual int ShopValue { get; } = 0;
    public virtual Sprite? ShopImage { get; }

    public virtual ItemUseCallback? OnActionUse { get; }
    public virtual Type? StaticItemType { get; }
    public virtual bool EnabledIf => true;

    public int SlotNumber { get; set; }


    public Skill()
    {
        GameManager.OnSystemRegistrationProcessCompleted += () =>
        {
            Player = GameManager.Player;
            Boss = GameManager.Boss;
            SkillID = (short)SkillSystem.GetRefCount();
            Debug.Log($"SkillID ({SkillID}) {{{SkillName}}}");
            SkillSystem.IncreaseRefCount();
        };
    }

    public void Enhance(float value)
    {
        EnhancementValueStack.Push(value);
    }

    public void LoseEnhancement()
    {
        EnhancementValueStack.Pop();
    }

    public void UseAction()
    {
        Condition? enableCondition = new(() => EnabledIf);
        if (enableCondition.WasMet == false) return;
        OnActionUse?.Invoke();
    }
}
