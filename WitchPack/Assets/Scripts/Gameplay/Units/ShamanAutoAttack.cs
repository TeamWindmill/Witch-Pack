using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShamanAA", menuName = "Ability/ShamanAA")]

public class ShamanAutoAttack : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        if (caster is Shaman)
        {
            Transform target = (caster as Shaman).EnemyTargeter.GetClosestTarget()?.transform;
            if (!ReferenceEquals(target, null))
            {
                TargetedShot newPew = GameManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
                newPew.transform.position = caster.transform.position;
                newPew.gameObject.SetActive(true);
                Vector2 dir = (target.position - caster.transform.position).normalized;
                newPew.Fire(caster, this, dir.normalized, target.position);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
        //enemy logic here (when necessary)
    }
}
