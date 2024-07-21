public class EnemyRangedAutoAttack : OffensiveAbility
{
    public readonly EnemyRangedAutoAttackSO Config;

    public EnemyRangedAutoAttack(EnemyRangedAutoAttackSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
    }
    
    public override bool CastAbility(out IDamagable target)
    {
        if ((Owner as Enemy)?.EnemyAI.ActiveState.GetType() == typeof(AttackCoreState))
        {
            target = LevelManager.Instance.CurrentLevel.CoreTemple;
        }
        else
        {
            target = Owner.ShamanTargetHelper.GetTarget(TargetData);
        }
        
        if (ReferenceEquals(target, null))
        {
            return false;
        }
        AutoAttackMono newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
        newPew.transform.position = Owner.CastPos.transform.position;
        newPew.gameObject.SetActive(true);
        newPew.Fire(Owner, this, target,Config.Speed);
        return true;
    }

    public override bool CheckCastAvailable()
    {
        if ((Owner as Enemy)?.EnemyAI.ActiveState.GetType() == typeof(AttackCoreState)) return true;
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }

}