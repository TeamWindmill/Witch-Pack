using Systems.Pool_System;

public class SmokeBomb : OffensiveAbility
{
    public readonly SmokeBombSO SmokeBombConfig;
    public SmokeBomb(SmokeBombSO config, BaseUnit owner) : base(config, owner)
    {
        SmokeBombConfig = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,config.Duration));
        abilityStats.Add(new AbilityStat(AbilityStatType.Size,config.Size));
    }

    public override bool CastAbility(out IDamagable target)
    {
        target = Owner.ShamanTargetHelper.GetTarget(TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (Owner.Stats[StatType.ThreatLevel].IntValue > target.Stats[StatType.ThreatLevel].IntValue) target = Owner;
            if (target.Stats[StatType.ThreatLevel].IntValue <= 0) return false;
            return Cast(Owner, target);
        }

        if (Owner.Stats[StatType.ThreatLevel].IntValue > 0) return Cast(Owner, Owner);
        
        return false;
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (target.Stats[StatType.ThreatLevel].IntValue > 0) return true;
        }

        if (Owner.Stats[StatType.ThreatLevel].IntValue > 0) return true;

        return false;
    }
    
    protected virtual bool Cast(BaseUnit caster, IDamagable target)
    {
        SmokeBombMono smokeBombMono = PoolManager.GetPooledObject<SmokeBombMono>();
        smokeBombMono.transform.position = target.GameObject.transform.position;
        smokeBombMono.gameObject.SetActive(true);
        smokeBombMono.SpawnBomb(this, caster);
        return true;
    }
}