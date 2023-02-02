#nullable enable
using System;

using Random = UnityEngine.Random;

/// <summary>
/// A highly enhanced version of Ether Alpha. Restores
/// between 25% and 50% of both your Health and Mana
/// </summary>
public class ITEtherOmega : Item, IHealthModifier, IManaModifier
{
    public override string ItemName => "Ether Omega";
    public override Type? StaticItemType => typeof(ITEtherOmega);
    public override ItemUseCallaback? OnActionUse => TakeEther;

    public float SetHealthBonus => Random.Range(25, 50);

    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    public HealthSystem? HealthSystem { get; set; }

    public float SetManaBonus => Random.Range(25, 50);

    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public ManaSystem? ManaSystem { get; set; }

    void TakeEther()
    {
        HealthSystem ??= GameManager.GetSystem<HealthSystem>();
        ManaSystem ??= GameManager.GetSystem<ManaSystem>();

        HealthSystem.SetHealth(nameof(PlayerEntity), GameManager.Player.MaxManaValue * ((IHealthModifier)this).SetHealthBonus, true);
        ManaSystem.SetMana(GameManager.Player.MaxManaValue * ((IManaModifier)this).SetManaBonus, true);
    }
}
