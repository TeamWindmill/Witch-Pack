public class SmokeBomb : OffensiveAbility
{
    public readonly SmokeBombSO SmokeBombConfig;
    public SmokeBomb(SmokeBombSO config, BaseUnit owner) : base(config, owner)
    {
        SmokeBombConfig = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(SmokeBombConfig.TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (Owner.Stats[StatType.ThreatLevel].Value > target.Stats[StatType.ThreatLevel].Value) target = Owner;
            if (target.Stats[StatType.ThreatLevel].Value <= 0) return false;
            return Cast(Owner, target);
        }

        if (Owner.Stats[StatType.ThreatLevel].Value > 0) return Cast(Owner, Owner);
        
        return false;
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(SmokeBombConfig.TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (target.Stats[StatType.ThreatLevel].Value > 0) return true;
        }

        if (Owner.Stats[StatType.ThreatLevel].Value > 0) return true;

        return false;
    }
    
    protected virtual bool Cast(BaseUnit caster, BaseUnit target)
    {
        SmokeBombMono smokeBombMono = LevelManager.Instance.PoolManager.SmokeBombPool.GetPooledObject();
        smokeBombMono.transform.position = target.transform.position;
        smokeBombMono.gameObject.SetActive(true);
        smokeBombMono.SpawnBomb(this, caster);
        return true;
    }
}