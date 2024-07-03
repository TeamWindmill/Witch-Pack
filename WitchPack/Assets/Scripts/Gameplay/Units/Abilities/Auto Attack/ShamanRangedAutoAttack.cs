public class ShamanRangedAutoAttack : OffensiveAbility
{
    private RangedAutoAttackSO _config;
    
    public ShamanRangedAutoAttack(RangedAutoAttackSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (ReferenceEquals(target, null))
        {
            return false;
        }

        AutoAttackMono newPew = LevelManager.Instance.PoolManager.ShamanAutoAttackPool.GetPooledObject();
        newPew.transform.position = Owner.CastPos.transform.position;
        newPew.gameObject.SetActive(true);
        newPew.Fire(Owner, this, target, _config.Speed);
        return true;
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}