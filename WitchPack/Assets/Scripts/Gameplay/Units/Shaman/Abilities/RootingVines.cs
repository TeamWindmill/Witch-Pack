using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/RootingVines")]

public class RootingVines : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.TargetHelper.GetTarget(caster.Targeter.AvailableTargets, TargetData);
        if (!ReferenceEquals(target, null))
        {
            RootingVinesMono newVines = LevelManager.Instance.PoolManager.RootingVinesPool.GetPooledObject();
            newVines.transform.position = target.transform.position;
            newVines.gameObject.SetActive(true);            
            return true;
        }
        else
        {
            return false;
        }

    }
}
