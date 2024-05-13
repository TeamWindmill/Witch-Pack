public class RootingVines : OffensiveAbility
{
    private RootingVinesSO _config;
    public RootingVines(RootingVinesSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);    
        if (!ReferenceEquals(target, null))
        {
            RootingVinesMono newVines = LevelManager.Instance.PoolManager.RootingVinesPool.GetPooledObject();
            newVines.Init(Owner, _config, _config.LastingTime,_config.AoeScale);
            newVines.transform.position = target.transform.position;
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
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        return !ReferenceEquals(target, null);
    }
}