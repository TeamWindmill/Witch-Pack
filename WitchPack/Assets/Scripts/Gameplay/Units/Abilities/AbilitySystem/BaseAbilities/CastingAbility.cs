using System.Collections.Generic;

public abstract class CastingAbility : Ability
{
    public CastingAbilitySO CastingConfig { get; }
    public List<StatusEffectData> StatusEffects { get; } = new();

    protected CastingAbility(CastingAbilitySO config, BaseUnit owner) : base(config,owner)
    {
        CastingConfig = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Cooldown,config.Cd));
        abilityStats.Add(new AbilityStat(AbilityStatType.CastTime,config.CastTime));
        if(config.GivesEnergyPoints)
            abilityStats.Add(new AbilityStat(AbilityStatType.EnergyPointsOnKill,config.EnergyPoints));

        foreach (var statusEffect in config.StatusEffects)
        {
            StatusEffects.Add(new StatusEffectData(statusEffect));
        }
    }
    
    public abstract bool CastAbility();
    public abstract bool CheckCastAvailable();

    public override void AddStatUpgrade(AbilityUpgradeConfig abilityUpgradeConfig)
    {
        base.AddStatUpgrade(abilityUpgradeConfig);
        
        if(abilityUpgradeConfig.StatusEffectUpgrades.Length <= 0) return;
        
        foreach (var statusEffect in StatusEffects)
        {
            foreach (var statusEffectUpgrade in abilityUpgradeConfig.StatusEffectUpgrades)
            {
                if (statusEffect.StatTypeAffected == statusEffectUpgrade.StatType && statusEffect.Process == statusEffectUpgrade.Process)
                {
                    statusEffect.Upgrades.Add(statusEffectUpgrade);
                }
            }
        }
    }

    public override void AddStatUpgrade(StatUpgradeConfig statUpgradeConfig)
    {
        base.AddStatUpgrade(statUpgradeConfig);
        
        if(statUpgradeConfig.StatusEffectUpgrades.Length <= 0) return;
        
        foreach (var statusEffect in StatusEffects)
        {
            foreach (var statusEffectUpgrade in statUpgradeConfig.StatusEffectUpgrades)
            {
                if (statusEffect.StatTypeAffected == statusEffectUpgrade.StatType && statusEffect.Process == statusEffectUpgrade.Process)
                {
                    statusEffect.Upgrades.Add(statusEffectUpgrade);
                }
            }
        }
    }
}