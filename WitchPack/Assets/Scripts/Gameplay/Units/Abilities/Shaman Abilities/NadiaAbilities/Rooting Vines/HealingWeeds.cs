using System.Collections.Generic;

public class HealingWeeds : OffensiveAbility
{
    public readonly HealingWeedsSO Config;
    public List<StatusEffectData> HealStatusEffects = new();
    public HealingWeeds(HealingWeedsSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,config.LastingTime));
        abilityStats.Add(new AbilityStat(AbilityStatType.Size,config.AoeScale));
        foreach (var effectConfig in config.HealStatusEffects)
        {
            HealStatusEffects.Add(new StatusEffectData(effectConfig));
        }
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            HealingWeedsMono newHealingWeeds = LevelManager.Instance.PoolManager.HealingWeedsPool.GetPooledObject();
            newHealingWeeds.Init(Owner, this, GetAbilityStatValue(AbilityStatType.Duration),GetAbilityStatValue(AbilityStatType.Size));
            newHealingWeeds.transform.position = target.transform.position;
            newHealingWeeds.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }

    public override void AddStatUpgrade(AbilityUpgradeConfig abilityUpgradeConfig)
    {
        base.AddStatUpgrade(abilityUpgradeConfig);
        
        //heal status effects upgrades
        foreach (var statusEffect in HealStatusEffects)
        {
            foreach (var statusEffectUpgrade in abilityUpgradeConfig.StatusEffectUpgrades)
            {
                if (statusEffect.StatTypeAffected == statusEffectUpgrade.StatType && statusEffect.Process == statusEffectUpgrade.Process)
                {
                    statusEffect.AddUpgrade(statusEffectUpgrade);
                }
            }
        }
    }
}