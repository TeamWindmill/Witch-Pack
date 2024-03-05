using System.Collections;
using System.Collections.Generic;
using Gameplay.Units.Abilities;
using UnityEngine;



[CreateAssetMenu(fileName = "ability", menuName = "Ability/HealingWeeds")]
public class HealingWeeds : OffensiveAbility
{
    [SerializeField] private float lastingTime;

    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData,LevelManager.Instance.CharmedEnemies);
        if (!ReferenceEquals(target, null))
        {
            HealingWeedsMono newHealingWeeds = LevelManager.Instance.PoolManager.HealingWeedsPool.GetPooledObject();
            newHealingWeeds.Init(caster, this, lastingTime);
            newHealingWeeds.transform.position = target.transform.position;
            newHealingWeeds.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData,LevelManager.Instance.CharmedEnemies);
        return !ReferenceEquals(target, null);
    }
}
