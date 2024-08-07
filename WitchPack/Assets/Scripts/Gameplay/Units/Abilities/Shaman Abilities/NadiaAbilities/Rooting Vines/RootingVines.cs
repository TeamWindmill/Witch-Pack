using Systems.Pool_System;

public class RootingVines : OffensiveAbility
{
    public readonly RootingVinesSO Config;
    public RootingVines(RootingVinesSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,config.LastingTime));
        abilityStats.Add(new AbilityStat(AbilityStatType.Size,config.AoeScale));
    }

    public override bool CastAbility(out IDamagable target)
    {
        target = Owner.EnemyTargetHelper.GetTarget(TargetData);    
        if (!ReferenceEquals(target, null))
        {
            RootingVinesMono newVines = PoolManager.GetPooledObject<RootingVinesMono>();
            newVines.Init(Owner, this, GetAbilityStatValue(AbilityStatType.Duration) ,GetAbilityStatValue(AbilityStatType.Size));
            newVines.transform.position = target.GameObject.transform.position;
            newVines.gameObject.SetActive(true);
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
}