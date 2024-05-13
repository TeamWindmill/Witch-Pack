public class PoisonIvy : OffensiveAbility
{
    private PoisonIvySO _config;
    public PoisonIvy(PoisonIvySO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        if (!ReferenceEquals(target, null))
        {
            PoisonIvyMono newIvyPoison = LevelManager.Instance.PoolManager.PoisonIvyPool.GetPooledObject();
            newIvyPoison.Init(Owner, _config, _config.LastingTime,_config.AoeScale);
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
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        return !ReferenceEquals(target, null);
    }
}