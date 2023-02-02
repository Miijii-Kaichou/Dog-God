#nullable enable

using System;
using Random = UnityEngine.Random;

/// <summary>
/// An enhanced version of the ALlpha version of a normal potion. Very potent.
/// So much in fact it'll restore 25% to 50% of your health
/// </summary>
public sealed class ITPotionOmega : Item, IHealthModifier
{
    public override string ItemName => "Potion Omega";
    public override Type? StaticItemType => typeof(ITPotionOmega);
    public override ItemUseCallaback? OnActionUse => TakePotion;

    public HealthSystem? HealthSystem { get; set; }

    public float SetHealthBonus => Random.Range(25, 50);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    private void TakePotion()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        HealthSystem.SetHealth(nameof(PlayerEntity), GameManager.Player.MaxHealthValue * ((IHealthModifier)this).HealthBonus, true);
    }
}