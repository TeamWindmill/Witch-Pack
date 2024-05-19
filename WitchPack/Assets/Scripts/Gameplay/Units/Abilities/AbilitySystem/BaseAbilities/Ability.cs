using System;
using System.Collections.Generic;

public abstract class Ability
{
    public AbilitySO BaseConfig { get; }
    public AbilityUpgradeState AbilityUpgradeState { get; private set; }
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
    
    public void ChangeUpgradeState(AbilityUpgradeState state)
    {
        AbilityUpgradeState = state;
    }

    public void UpgradeAbility()
    {
        if (AbilityUpgradeState != AbilityUpgradeState.Open) return;
        ChangeUpgradeState(AbilityUpgradeState.Upgraded);
        foreach (var abilityUpgrade in Upgrades)
        {
            abilityUpgrade.ChangeUpgradeState(AbilityUpgradeState.Open);
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

    protected float GetAbilityStatValue(AbilityStatType abilityStatType)
    {
        foreach (var abilityStat in abilityStats)
        {
            if (abilityStat.StatType == abilityStatType) return abilityStat.GetStatValue();
        }

        throw new Exception("ability stat not found in ability");
    }

    public void AddStatUpgrade(AbilityStatUpgrade abilityStatUpgrade)
    {
        foreach (var stat in abilityStats)
        {
            if (stat.StatType == abilityStatUpgrade.StatType)
            {
                stat.AddModifier(abilityStatUpgrade.AbilityStatValue);
                return;
            }
        }
    }

}