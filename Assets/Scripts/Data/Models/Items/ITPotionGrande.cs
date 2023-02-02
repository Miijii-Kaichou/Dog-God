#nullable enable

using System;

/// <summary>
/// A larger sized potion (still not very potent). Restores 1000 HP
/// </summary>
public sealed class ITPotionGrande : Item, IHealthModifier
{
    public override string ItemName => "Potion Grande";
    public override Type? StaticItemType => typeof(ITPotionGrande);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public HealthSystem? HealthSystem { get; set; }

    public float SetHealthBonus => 1000;

    public BonusModificationType HealthModificationType => BonusModificationType.Whole;

    private void TakePotion()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        HealthSystem.SetHealth(nameof(PlayerEntity), ((IHealthModifier)this).HealthBonus, true);
    }
}
