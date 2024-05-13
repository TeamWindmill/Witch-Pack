using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmokeBomb", menuName = "Ability/SmokeBomb/SmokeBomb")]
public class SmokeBombSO : OffensiveAbilitySO
{
    public float Duration => _duration;

    [SerializeField] private float _duration;

    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (caster.Stats.ThreatLevel > target.Stats.ThreatLevel) target = caster;
            if (target.Stats.ThreatLevel <= 0) return false;
            return Cast(caster, target);
        }

        if (caster.Stats.ThreatLevel > 0) return Cast(caster, caster);
        
        return false;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);

        if (!ReferenceEquals(target, null))
        {
            if (target.Stats.ThreatLevel > 0) return true;
        }

        if (caster.Stats.ThreatLevel > 0) return true;

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