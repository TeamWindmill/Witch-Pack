using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/RootingVines")]

public class RootingVines : OffensiveAbility
{
    [SerializeField] private float radius;
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.TargetHelper.GetTarget(caster.Targeter.AvailableTargets, TargetData);    
        if (!ReferenceEquals(target, null))
        {
            target.Damageable.GetHit(caster.DamageDealer, this);
            List<BaseUnit> colleteral = caster.Targeter.GetAvailableTargets(target, radius);
            foreach (var enemy in colleteral)
            {
                enemy.Damageable.GetHit(caster.DamageDealer, this);
            }
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
