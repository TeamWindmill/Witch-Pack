using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmokeBomb", menuName = "Ability/SmokeBomb/SmokeBomb")]
public class SmokeBombSO : OffensiveAbility
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
        SmokeBomb smokeBomb = LevelManager.Instance.PoolManager.SmokeBombPool.GetPooledObject();
        smokeBomb.transform.position = target.transform.position;
        smokeBomb.gameObject.SetActive(true);
        smokeBomb.SpawnBomb(this, caster);
        return true;
    }
}