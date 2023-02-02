#nullable enable

// A normal potion that's not very potent. Restores 500 Hp
using UnityEngine;

public sealed class ITPotion : Item, IHealthModifier
{
    public float SetHealthBonus => 5;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    HealthSystem? _healthSystem;

    public override ItemUseCallaback? OnActionUse => TakePotion;

    public void TakePotion()
    {
        _healthSystem ??= GameManager.GetSystem<HealthSystem>();
        Debug.Log("Taking Potion");
        _healthSystem.SetHealth(nameof(PlayerEntity), ((IHealthModifier)this).HealthBonus, isRelative: true);
    }
}