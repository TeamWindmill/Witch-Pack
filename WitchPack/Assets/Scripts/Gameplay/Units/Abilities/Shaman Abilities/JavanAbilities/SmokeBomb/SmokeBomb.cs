public class SmokeBomb : OffensiveAbility
{
    private SmokeBombSO _config;
    public SmokeBomb(SmokeBombSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(_config.TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (Owner.Stats.ThreatLevel > target.Stats.ThreatLevel) target = Owner;
            if (target.Stats.ThreatLevel <= 0) return false;
            return Cast(Owner, target);
        }

        if (Owner.Stats.ThreatLevel > 0) return Cast(Owner, Owner);
        
        return false;
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(_config.TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (target.Stats.ThreatLevel > 0) return true;
        }

        if (Owner.Stats.ThreatLevel > 0) return true;

        return false;
    }
    
    protected virtual bool Cast(BaseUnit caster, BaseUnit target)
    {
        SmokeBombMono smokeBombMono = LevelManager.Instance.PoolManager.SmokeBombPool.GetPooledObject();
        smokeBombMono.transform.position = target.transform.position;
        smokeBombMono.gameObject.SetActive(true);
        smokeBombMono.SpawnBomb(_config, caster);
        return true;
    }
}