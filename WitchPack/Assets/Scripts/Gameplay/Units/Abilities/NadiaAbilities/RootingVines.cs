using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/RootingVines")]

public class RootingVines : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);    
        if (!ReferenceEquals(target, null))
        {
            RootingVinesMono newVines = LevelManager.Instance.PoolManager.RootingVinesPool.GetPooledObject();
            newVines.Init(caster, this);
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
