using System;
using System.Collections.Generic;

public abstract class Ability
{
    public AbilitySO BaseConfig { get; }
    public UpgradeState UpgradeState { get; private set; }
    public List<Ability> Upgrades { get; } = new();
    protected BaseUnit Owner { get; }
    
    protected List<AbilityStat> abilityStats = new();

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

    public float GetAbilityStatValue(AbilityStatType abilityStatType)
    {
        foreach (var abilityStat in abilityStats)
        {
            if (abilityStat.StatType == abilityStatType) return abilityStat.Value;
        }

        throw new Exception("ability stat not found in ability");
    }

    public virtual void AddStatUpgrade(AbilityUpgradeConfig abilityUpgradeConfig)
    {
        foreach (var stat in abilityStats)
        {
            if (stat.StatType == abilityUpgradeConfig.StatType)
            {
                switch (abilityUpgradeConfig.Factor)
                {
                    case Factor.Add:
                        stat.AddModifier(abilityUpgradeConfig.StatValue);
                        break;
                    case Factor.Subtract:
                        stat.AddModifier(-abilityUpgradeConfig.StatValue);
                        break;
                    case Factor.Multiply:
                        stat.AddMultiplier((100 + abilityUpgradeConfig.StatValue) /100);
                        break;
                    case Factor.Divide:
                        stat.AddMultiplier(-(100 + abilityUpgradeConfig.StatValue) /100);
                        break;
                }
                return;
            }
        }
    }
    public virtual void AddStatUpgrade(StatUpgradeConfig statUpgradeConfig)
    {
        foreach (var stat in abilityStats)
        {
            if (stat.StatType == statUpgradeConfig.AbilityStatType)
            {
                switch (statUpgradeConfig.Factor)
                {
                    case Factor.Add:
                        stat.AddModifier(statUpgradeConfig.StatValue);
                        break;
                    case Factor.Subtract:
                        stat.AddModifier(-statUpgradeConfig.StatValue);
                        break;
                    case Factor.Multiply:
                        stat.AddMultiplier(statUpgradeConfig.StatValue);
                        break;
                    case Factor.Divide:
                        stat.AddMultiplier(1 / statUpgradeConfig.StatValue);
                        break;
                }
                return;
            }
        }
    }

}