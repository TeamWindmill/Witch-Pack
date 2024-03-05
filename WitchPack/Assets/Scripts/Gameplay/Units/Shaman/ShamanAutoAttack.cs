using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Gameplay.Units.Abilities;

[CreateAssetMenu(fileName = "ShamanAA", menuName = "Ability/ShamanAA")]
public class ShamanAutoAttack : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData,LevelManager.Instance.CharmedEnemies);
        if (ReferenceEquals(target, null))
        {
            return false;
        }
        ShamanAutoAttackMono newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
        newPew.transform.position = caster.CastPos.transform.position;
        newPew.gameObject.SetActive(true);
        Vector2 dir = (target.transform.position - caster.transform.position).normalized;
        newPew.Fire(caster, this, dir.normalized, target);
        return true;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData,LevelManager.Instance.CharmedEnemies);
        return !ReferenceEquals(target, null);
    }
}
