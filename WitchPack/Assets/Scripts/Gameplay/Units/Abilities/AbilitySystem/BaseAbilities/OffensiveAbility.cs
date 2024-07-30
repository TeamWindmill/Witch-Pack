using System.Collections.Generic;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseAbilities
{
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

        public override void AddStatUpgrade(StatMetaUpgradeConfig statMetaUpgradeConfig)
        {
            base.AddStatUpgrade(statMetaUpgradeConfig);
            DamageBoosts.AddRange(statMetaUpgradeConfig.DamageBoosts);
        }
    }
}