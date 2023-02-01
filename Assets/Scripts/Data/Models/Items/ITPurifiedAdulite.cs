using static SharedData.Constants;
using Random = UnityEngine.Random;
using Extensions;

#nullable enable

/* It's Adulite, but completely purified. 
   Gain 3 levels as you restore all your MP and HP,
   whilst regenerating it between 8% and 12% for 3 whole minutes.*/
public sealed class ITPurifiedAdulite : Item, IRegisterEntity<PlayerEntity>, IHealthModifier, IManaModifier, ILevelModifier, IUseLifeCycle
{
    public override string? ItemName => "Purified Adulite";

    // We use the SetHealthBonus to know the value we want to work with
    public float SetHealthBonus => Random.Range(8, 12);
    public BonusModificationType HealthModificationType => BonusModificationType.PercentageOf;

    // Same goes for the SetManaBonus
    public float SetManaBonus => Random.Range(8, 12);
    public BonusModificationType ManaModificationType => BonusModificationType.PercentageOf;

    public int LevelGain => 3;

    public PlayerEntity? EntityReference { get; set; }

    public float LifeDuration => MinutesInSeconds * 3;

    // We'll regenerate every seconds
    public float TickDuration => 1;

    public override ItemUseCallaback? OnActionUse => OnUse;


    HealthSystem? _healthSystem;
    ManaSystem? _manaSystem;
    LevelingSystem? _levelSystem;

    public ITPurifiedAdulite()
    {
        GameManager.OnSystemRegistrationProcessCompleted = () =>
        {
            _healthSystem = GameManager.GetSystem<HealthSystem>();
            _manaSystem = GameManager.GetSystem<ManaSystem>();
            _levelSystem = GameManager.GetSystem<LevelingSystem>();
        };
    }

    public void OnUse()
    {
        // Restore all HP and MP on Use
        // Then increase the player's level up 3
        _healthSystem.RestoreAllHealth(nameof(PlayerEntity));
        _manaSystem.RestoreAllMana();
        _levelSystem.IncreaseToLevel(LevelGain);
    }

    void OnTick()
    {
        if (EntityReference == null ||
            _healthSystem == null ||
            _manaSystem == null ||
            _levelSystem == null) return;

        // Regain HP and Mana between 8% and  12%

        // We use HealthBonus (instead of SetHealthBonus) for our final bonus 
        // which is influenced by our HealthModificationType (which is percent of.
        // We want to increase our health with a certain percentage of our MaxHealth
        _healthSystem.SetHealth(nameof(PlayerEntity), _healthSystem[nameof(PlayerEntity)].MaxHealthValue * ((IHealthModifier)this).HealthBonus, isRelative: true);

        // Same for Mana.
        _manaSystem.SetMana(EntityReference.MaxManaValue * ((IManaModifier)this).ManaBonus, isRelative: true);
    }

    // Congratualations! You've just created the Purified Adulite item.
}
