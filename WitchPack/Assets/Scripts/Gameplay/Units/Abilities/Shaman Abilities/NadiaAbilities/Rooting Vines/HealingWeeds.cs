public class HealingWeeds : OffensiveAbility
{
    public readonly HealingWeedsSO Config;
    public HealingWeeds(HealingWeedsSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        if (!ReferenceEquals(target, null))
        {
            HealingWeedsMono newHealingWeeds = LevelManager.Instance.PoolManager.HealingWeedsPool.GetPooledObject();
            newHealingWeeds.Init(Owner, this, Config.LastingTime,Config.AoeScale);
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
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        return !ReferenceEquals(target, null);
    }
}