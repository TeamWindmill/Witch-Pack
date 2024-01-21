using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Test")]

public class PiercingShot : OffensiveAbility
{
    // testing simple projectile 

    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.TargetHelper.GetTarget(caster.Targeter.AvailableTargets, TargetData);
        if (!ReferenceEquals(target, null))
        {
            Projectile newPew = LevelManager.Instance.PoolManager.TestAbilityPool.GetPooledObject();
            newPew.transform.position = caster.transform.position;
            newPew.gameObject.SetActive(true);
            Vector2 dir = (target.transform.position - caster.transform.position) / (target.transform.position - caster.transform.position).magnitude;
            newPew.Fire(caster, this, dir.normalized);
            return true;
        }
        else
        {
            return false;
        }

    }

}
