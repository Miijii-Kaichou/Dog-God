#nullable enable

using System;

using Random = UnityEngine.Random;

/// <summary>
/// An enhanced version of a normal potion. Potent enough to restore
/// between 10% and 15% of your health
/// </summary>
public sealed class ITPotionAlpha : Item, IHealthModifier
{
    public override string ItemName => "Potion Alpha";
    public override Type? StaticItemType => typeof(ITPotionAlpha);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public HealthSystem? HealthSystem { get; set; }

    public float SetHealthBonus => Random.Range(10, 15);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    private void TakePotion()
    {
       HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        HealthSystem.SetHealth(nameof(PlayerEntity), GameManager.Player.MaxHealthValue * ((IHealthModifier)this).HealthBonus, true);
    }
}
