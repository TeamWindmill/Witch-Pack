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
            Owner.Stats.AddValueToStat(increase.StatType, increase.Value);
        }
    }

    public void AddPassiveStatUpgrade(StatUpgradeConfig statUpgradeConfig)
    {
        foreach (var stat in PassiveAbilityStats)
        {
            foreach (var statConfig in statUpgradeConfig.Stats)
            {
                if (stat.StatType == statConfig.StatType)
                {
                    switch (statConfig.Factor)
                    {
                        case Factor.Add:
                            stat.AddModifier(statConfig.StatValue);
                            break;
                        case Factor.Subtract:
                            stat.AddModifier(-statConfig.StatValue);
                            break;
                        case Factor.Multiply:
                            stat.AddMultiplier(statConfig.StatValue/100);
                            break;
                        case Factor.Divide:
                            stat.AddMultiplier(-(statConfig.StatValue/100));
                            break;
                    }
                    return;
                }
            }
        }
    }
}