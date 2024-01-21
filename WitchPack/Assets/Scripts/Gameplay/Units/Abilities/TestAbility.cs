using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Test")]

public class TestAbility : OffensiveAbility
{
    // testing simple projectile 

    public override bool CastAbility(BaseUnit caster, Transform target = null)
    {
        if (caster is Shaman)
        {
            // Transform target = (caster as Shaman).EnemyTargeter.GetClosestTarget()?.transform;
            // if (!ReferenceEquals(target, null))
            // {
            //     Projectile newPew = LevelManager.Instance.PoolManager.TestAbilityPool.GetPooledObject();
            //     newPew.transform.position = caster.transform.position;
            //     newPew.gameObject.SetActive(true);
            //     Vector2 dir = (target.position - caster.transform.position) / (target.position - caster.transform.position).magnitude;
            //     newPew.Fire(caster, this, dir.normalized);
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
        }
        return false;
        //enemy logic here (when necessary)
       
        
      
       
    }

}
