using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ShamanAA", menuName = "Ability/ShamanAA")]

public class ShamanAutoAttack : OffensiveAbility
{
    public override bool CastAbility(BaseUnit caster, Transform target = null)
    {
        if (caster is Shaman shaman)
        {
            // if (!ReferenceEquals(targets, null) && targets.Count > 0)
            // {
            //     TargetedShot newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
            //     newPew.transform.position = shaman.CastPos.transform.position;
            //     newPew.gameObject.SetActive(true);
            //     Vector2 dir = (target.transform.position - caster.transform.position).normalized;
            //     newPew.Fire(caster, this, dir.normalized, target);
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
        }
        return false;
        //enemy logic here (when necessary)
    }
}
