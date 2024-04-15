using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRangedAutoAttack", menuName = "Ability/AutoAttack/EnemyRangedAutoAttack")]
public class EnemyRangedAutoAttack : AutoAttack
{
    [SerializeField] private int coreDamage;
    public override int CoreDamage => coreDamage;

    public override bool CastAbility(BaseUnit caster)
    {
        IDamagable target = null;
        if ((caster as Enemy)?.EnemyAI.ActiveState.GetType() == typeof(AttackCoreState))
        {
            target = LevelManager.Instance.CurrentLevel.CoreTemple;
        }
        else
        {
            target = caster.ShamanTargetHelper.GetTarget(TargetData);
        }
        
        if (ReferenceEquals(target, null))
        {
            return false;
        }
        AutoAttackMono newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
        newPew.transform.position = caster.CastPos.transform.position;
        newPew.gameObject.SetActive(true);
        newPew.Fire(caster, this, target);
        return true;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        if ((caster as Enemy)?.EnemyAI.ActiveState.GetType() == typeof(AttackCoreState)) return true;
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}