public class PoisonIvy : OffensiveAbility
{
    public readonly PoisonIvySO Config;
    public PoisonIvy(PoisonIvySO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Damage,config.PoisonDamage));
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,config.LastingTime));
        abilityStats.Add(new AbilityStat(AbilityStatType.Size,config.AoeScale));
        abilityStats.Add(new AbilityStat(AbilityStatType.DotDamage,config.PoisonDamage));
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            PoisonIvyMono newIvyPoison = LevelManager.Instance.PoolManager.PoisonIvyPool.GetPooledObject();
            newIvyPoison.Init(Owner, this, GetAbilityStatValue(AbilityStatType.Duration),GetAbilityStatValue(AbilityStatType.Size));
            newIvyPoison.transform.position = target.transform.position;
            newIvyPoison.gameObject.SetActive(true);
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