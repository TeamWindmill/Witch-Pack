using UnityEngine;

[CreateAssetMenu(fileName = "HighImpact", menuName = "Ability/SmokeBomb/HighImpact")]
public class HighImpactSO : SmokeBombSO
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        
        if (!ReferenceEquals(target, null))
        {
            if (caster.Stats.ThreatLevel > target.Stats.ThreatLevel) target = caster;
            return Cast(caster, target);
        }
        else
        {
            if(caster.Stats.ThreatLevel > 0) return Cast(caster, caster);
            else return false;
        }
    }

    private bool Cast(BaseUnit caster, BaseUnit target)
    {
        HighImpactSmokeBomb highImpact = LevelManager.Instance.PoolManager.HighImpactPool.GetPooledObject();
        highImpact.transform.position = target.transform.position;
        highImpact.gameObject.SetActive(true);
        highImpact.SpawnBomb(this, caster);
        return true;
    }
}