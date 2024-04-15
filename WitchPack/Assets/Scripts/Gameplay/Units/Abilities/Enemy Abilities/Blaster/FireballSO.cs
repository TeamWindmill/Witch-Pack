using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/EnemyAbilities/Fireball")]
public class FireballSO : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        var target = caster.ShamanTargetHelper.GetTarget(TargetData);
        if (ReferenceEquals(target, null))
        {
            return false;
        }

        FireballMono fireball = LevelManager.Instance.PoolManager.FireballPool.GetPooledObject();
        fireball.transform.position = caster.CastPos.transform.position;
        fireball.gameObject.SetActive(true);
        fireball.Fire(caster, this, target);
        return true;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}