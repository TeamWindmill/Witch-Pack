using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ShamanAA", menuName = "Ability/ShamanAA")]

public class ShamanAutoAttack : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        if (caster is Shaman)
        {
            Shaman shaman = caster as Shaman;
            List<Enemy> targets = shaman.EnemyTargeter.GetAvailableTargets(shaman.transform.position, Range);
            if (!ReferenceEquals(targets, null) && targets.Count > 0)
            {
                TargetedShot newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
                newPew.transform.position = caster.transform.position;
                newPew.gameObject.SetActive(true);
                BaseUnit target = shaman.TargetHelper.GetTarget(targets.Cast<BaseUnit>().ToList(), TargetData);
                Vector2 dir = (target.transform.position - caster.transform.position).normalized;
                newPew.Fire(caster, this, dir.normalized, target);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
        //enemy logic here (when necessary)
    }
}
