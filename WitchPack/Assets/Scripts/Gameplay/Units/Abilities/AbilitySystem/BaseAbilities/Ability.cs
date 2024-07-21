using System;
using System.Collections.Generic;

public abstract class Ability
{
    public AbilitySO BaseConfig { get; }
    public UpgradeState UpgradeState { get; private set; }
    public List<Ability> Upgrades { get; } = new();
    protected BaseUnit Owner { get; }
    
    protected List<AbilityStat> abilityStats = new();    
    protected List<AbilityBehavior> _abilitiesBehaviors = new();

    protected Ability(AbilitySO baseConfig, BaseUnit owner)
    {
        BaseConfig = baseConfig;
        Owner = owner;

        foreach (var upgrade in BaseConfig.Upgrades)
        {
            Upgrades.Add(AbilityFactory.CreateAbility(upgrade,Owner));
        }
    }
    
    public void ChangeUpgradeState(UpgradeState state)
    {
        UpgradeState = state;
    }

    public void UpgradeAbility()
    {
        if (UpgradeState != UpgradeState.Open) return;
        ChangeUpgradeState(UpgradeState.Upgraded);
        foreach (var abilityUpgrade in Upgrades)
        {
            abilityUpgrade.ChangeUpgradeState(UpgradeState.Open);
        }
    }

    public AbilityStat GetAbilityStat(AbilityStatType statType)
    {
        foreach (var abilityStat in abilityStats)
        {
            if(abilityStat.StatType == statType) return abilityStat;
        }
        return null;
    }
    
    public List<Ability> GetUpgrades()
    {
        var upgrades = new List<Ability>();
        foreach (var upgrade in Upgrades)
        {
            upgrades.Add(upgrade);
            foreach (var secondUpgrade in upgrade.Upgrades)
            {
                upgrades.Add(secondUpgrade);
            }
        }

        return upgrades;
    }

    public virtual bool HasAbilityBehavior(AbilityBehavior abilityBehavior)
    {
        foreach (var behavior in _abilitiesBehaviors)
        {
            if (behavior == abilityBehavior) return true;
        }

        return false;
    }
    

    public virtual void AddAbilityBehavior(AbilityUpgradeConfig abilityUpgradeConfig)
    {
        foreach (var behavior in abilityUpgradeConfig.AbilitiesBehaviors)
        {
            _abilitiesBehaviors.Add(behavior);
        }
    }
    public virtual void AddAbilityBehavior(StatMetaUpgradeConfig statMetaUpgradeConfig)
    {
        foreach (var behavior in statMetaUpgradeConfig.AbilitiesBehaviors)
        {
            _abilitiesBehaviors.Add(behavior);
        }
    }
    public float GetAbilityStatValue(AbilityStatType abilityStatType)
    {
        foreach (var abilityStat in abilityStats)
        {
            if (abilityStat.StatType == abilityStatType) return abilityStat.Value;
        }

        throw new Exception("ability stat not found in ability");
    }
    public int GetAbilityStatIntValue(AbilityStatType abilityStatType)
    {
        foreach (var abilityStat in abilityStats)
        {
            if (abilityStat.StatType == abilityStatType) return abilityStat.IntValue;
        }

        throw new Exception("ability stat not found in ability");
    }

    public virtual void AddStatUpgrade(AbilityUpgradeConfig abilityUpgradeConfig)
    {
        foreach (var stat in abilityStats)
        {
            foreach (var statConfig in abilityUpgradeConfig.Stats)
            {
                if (stat.StatType == statConfig.StatType)
                {
                    stat.AddStatValue(statConfig.Factor,statConfig.StatValue);
                }
            }
        }
    }
    public virtual void AddStatUpgrade(StatMetaUpgradeConfig statMetaUpgradeConfig)
    {
        foreach (var stat in abilityStats)
        {
            foreach (var abilityStatConfig in statMetaUpgradeConfig.AbilityStats)
            {
                if (stat.StatType == abilityStatConfig.StatType)
                {
                    stat.AddStatValue(abilityStatConfig.Factor,abilityStatConfig.StatValue);
                }
            }
        }
    }

}