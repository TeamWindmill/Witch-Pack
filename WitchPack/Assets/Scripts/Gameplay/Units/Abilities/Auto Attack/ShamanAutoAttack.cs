using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ShamanAutoAttack", menuName = "Ability/AutoAttack/ShamanAutoAttack")]
public class ShamanAutoAttack : AutoAttack
{
    [BoxGroup("Auto Attack")][SerializeField] private float _speed;
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
        newPew.Fire(caster, this, target,_speed);
        return true;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}
