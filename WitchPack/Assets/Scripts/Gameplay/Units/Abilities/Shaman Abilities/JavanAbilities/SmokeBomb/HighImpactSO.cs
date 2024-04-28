using UnityEngine;

[CreateAssetMenu(fileName = "HighImpact", menuName = "Ability/SmokeBomb/HighImpact")]
public class HighImpactSO : SmokeBombSO
{

    protected override bool Cast(BaseUnit caster, BaseUnit target)
    {
        HighImpactSmokeBomb highImpact = LevelManager.Instance.PoolManager.HighImpactPool.GetPooledObject();
        highImpact.transform.position = target.transform.position;
        highImpact.gameObject.SetActive(true);
        highImpact.SpawnBomb(this, caster);
        return true;
    }
}