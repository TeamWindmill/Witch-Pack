using System.Collections.Generic;

public abstract class OffensiveAbility : CastingAbility
{
    public OffensiveAbilitySO OffensiveAbilityConfig { get; }
    public List<DamageBoostData> DamageBoosts { get; } = new();

    protected OffensiveAbility(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        OffensiveAbilityConfig = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Damage,config.BaseDamage));
        DamageBoosts.AddRange(config.DamageBoosts);
    }

    public override void AddStatUpgrade(AbilityUpgradeConfig abilityUpgradeConfig)
    {
        base.AddStatUpgrade(abilityUpgradeConfig);
        DamageBoosts.AddRange(abilityUpgradeConfig.DamageBoosts);
    }
}