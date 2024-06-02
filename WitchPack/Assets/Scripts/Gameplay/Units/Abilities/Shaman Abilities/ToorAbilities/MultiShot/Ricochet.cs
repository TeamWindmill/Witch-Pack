public class Ricochet : MultiShot
{
    public readonly RicochetSO Config;
    public Ricochet(RicochetSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.BounceAmount,config.BounceAmount));
    }

    protected override MultiShotMono GetPooledObject()
    {
        return LevelManager.Instance.PoolManager.RicochetPool.GetPooledObject();
    }
}