using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ShamanAutoAttack", menuName = "Ability/AutoAttack/ShamanAutoAttack")]
public class ShamanAutoAttack : AutoAttack
{
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (ReferenceEquals(target, null))
        {
            return false;
        }
        AutoAttackMono newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
        newPew.transform.position = caster.CastPos.transform.position;
        newPew.gameObject.SetActive(true);
        Vector2 dir = (target.transform.position - caster.transform.position).normalized;
        newPew.Fire(caster, this, dir.normalized, target);
        return true;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}
