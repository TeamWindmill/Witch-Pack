using System.Collections.Generic;

public class StatPassive : PassiveAbility
{
    private StatPassiveSO _config;
    
    protected List<Stat> PassiveAbilityStats = new();


    public StatPassive(StatPassiveSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
        foreach (var statIncrease in _config.StatIncreases)
        {
            PassiveAbilityStats.Add(new Stat(statIncrease.StatType,statIncrease.Value));
        }
        
    }

    public override void SubscribePassive()
    {
        foreach (var increase in PassiveAbilityStats)
        {
            Owner.Stats.AddModifierToStat(increase.StatType, increase.Value);
        }
    }

    public void AddPassiveStatUpgrade(StatMetaUpgradeConfig statMetaUpgradeConfig)
    {
        foreach (var stat in PassiveAbilityStats)
        {
            foreach (var statConfig in statMetaUpgradeConfig.Stats)
            {
                if (stat.StatType == statConfig.StatType)
                {
                    stat.AddStatValue(statConfig.Factor,statConfig.StatValue);
                    return;
                }
            }
        }
    }
}