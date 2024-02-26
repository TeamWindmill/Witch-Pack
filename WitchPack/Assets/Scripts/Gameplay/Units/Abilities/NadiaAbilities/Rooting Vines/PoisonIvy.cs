using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PoisonIvy")]

public class PoisonIvy : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            PoisonIvyMono newIvyPoison = LevelManager.Instance.PoolManager.PoisonIvyPool.GetPooledObject();
            newIvyPoison.Init(caster, this);
            newIvyPoison.transform.position = target.transform.position;
            newIvyPoison.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }

    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}
