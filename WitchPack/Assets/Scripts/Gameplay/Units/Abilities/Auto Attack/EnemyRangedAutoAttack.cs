public class EnemyRangedAutoAttack : OffensiveAbility
{
    private RangedAutoAttackSO _config;

    public EnemyRangedAutoAttack(EnemyRangedAutoAttackSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }
    
    public override bool CastAbility()
    {
        IDamagable target;
        if ((Owner as Enemy)?.EnemyAI.ActiveState.GetType() == typeof(AttackCoreState))
        {
            target = LevelManager.Instance.CurrentLevel.CoreTemple;
        }
        else
        {
            target = Owner.ShamanTargetHelper.GetTarget(CastingConfig.TargetData);
        }
        
        if (ReferenceEquals(target, null))
        {
            return false;
        }
        AutoAttackMono newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
        newPew.transform.position = Owner.CastPos.transform.position;
        newPew.gameObject.SetActive(true);
        newPew.Fire(Owner, _config, target,_config.Speed);
        return true;
    }

    public override bool CheckCastAvailable()
    {
        if ((Owner as Enemy)?.EnemyAI.ActiveState.GetType() == typeof(AttackCoreState)) return true;
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(CastingConfig.TargetData);
        return !ReferenceEquals(target, null);
    }

}