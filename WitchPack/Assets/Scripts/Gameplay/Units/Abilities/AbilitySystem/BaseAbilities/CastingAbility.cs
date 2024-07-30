using System.Collections.Generic;
using Gameplay.Targeter;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Stats;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseAbilities
{
    public abstract class CastingAbility : Ability
    {
        public CastingAbilitySO CastingConfig { get; }
        public List<StatusEffectData> StatusEffects { get; } = new();

        public TargetData TargetData => Owner.Stats[StatType.ThreatLevel].Value >= 1 ? CastingConfig.UnderAttackTargetData : CastingConfig.TargetData;

        protected CastingAbility(CastingAbilitySO config, BaseUnit owner) : base(config,owner)
        {
            CastingConfig = config;
            abilityStats.Add(new AbilityStat(AbilityStatType.Cooldown,config.Cd));
            abilityStats.Add(new AbilityStat(AbilityStatType.CastTime,config.CastTime));
            if(config.GivesEnergyPoints)
                abilityStats.Add(new AbilityStat(AbilityStatType.EnergyPointsOnCast,config.EnergyPoints));

            foreach (var statusEffect in config.StatusEffects)
            {
                StatusEffects.Add(new StatusEffectData(statusEffect));
            }
        }
    
        public abstract bool CastAbility(out IDamagable target);

        public virtual bool ManualCast()
        {
            return false;
        }
        public abstract bool CheckCastAvailable();

        public override void AddStatUpgrade(AbilityUpgradeConfig abilityUpgradeConfig)
        {
            base.AddStatUpgrade(abilityUpgradeConfig);
        
            //adding status effect upgrades
            if(abilityUpgradeConfig.StatusEffectUpgrades.Length <= 0) return;
        
            foreach (var statusEffect in StatusEffects)
            {
                foreach (var statusEffectUpgrade in abilityUpgradeConfig.StatusEffectUpgrades)
                {
                    if (statusEffect.StatusEffectVisual == statusEffectUpgrade.StatusEffectVisual && statusEffect.Process == statusEffectUpgrade.Process)
                    {
                        statusEffect.AddUpgrade(statusEffectUpgrade);
                    }
                }
            }
        }

        public override void AddStatUpgrade(StatMetaUpgradeConfig statMetaUpgradeConfig)
        {
            base.AddStatUpgrade(statMetaUpgradeConfig);
        
            //adding status effect upgrades
            if(statMetaUpgradeConfig.StatusEffectUpgrades.Length <= 0) return;
        
            foreach (var statusEffect in StatusEffects)
            {
                foreach (var statusEffectUpgrade in statMetaUpgradeConfig.StatusEffectUpgrades)
                {
                    if (statusEffect.StatusEffectVisual == statusEffectUpgrade.StatusEffectVisual && statusEffect.Process == statusEffectUpgrade.Process)
                    {
                        statusEffect.AddUpgrade(statusEffectUpgrade);
                    }
                }
            }
        }
    }
}