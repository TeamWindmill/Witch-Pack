using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ability", menuName = "Ability/HealingWeeds")]
public class HealingWeeds : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            HealingWeedsMono newHealingWeeds = LevelManager.Instance.PoolManager.HealingWeedsPool.GetPooledObject();
            newHealingWeeds.Init(caster, this);
            newHealingWeeds.transform.position = target.transform.position;
            newHealingWeeds.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }
}
