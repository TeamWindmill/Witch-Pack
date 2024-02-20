using UnityEngine;

[CreateAssetMenu(fileName = "HighImpact", menuName = "Ability/SmokeBomb/HighImpact")]
public class HighImpactSO : SmokeBombSO
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            HighImpactSmokeBomb highImpact = LevelManager.Instance.PoolManager.HighImpactPool.GetPooledObject();
            highImpact.transform.position = target.transform.position;
            highImpact.gameObject.SetActive(true);
            highImpact.SpawnBomb(this, caster);
            return true;
        }
        else
        {
            return false;
        }
    }
}