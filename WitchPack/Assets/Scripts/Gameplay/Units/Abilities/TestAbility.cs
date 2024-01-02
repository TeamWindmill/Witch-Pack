using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Test")]

public class TestAbility : OffensiveAbility
{
    // testing simple projectile 

    public override void CastAbility(BaseUnit caster)
    {
        Projectile newPew = GameManager.Instance.PoolManager.AutoAttackPool.GetPooledObject();
        newPew.transform.position = caster.transform.position;
        
        if (caster is Shaman)
        {
            Transform target = (caster as Shaman).EnemyTargeter.GetClosestTarget().transform;
            newPew.gameObject.SetActive(true);
            Vector2 dir = (target.position - caster.transform.position).normalized;
            newPew.Fire(caster, this, dir);
        }
       
    }

}
