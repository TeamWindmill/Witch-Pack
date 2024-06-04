public class Fireball : OffensiveAbility
{
    public readonly FireballSO Config;
    
    public Fireball(FireballSO config, BaseUnit owner) : base(config, owner)
    {
        Config = config;
    }

    public override bool CastAbility()
    {
        var target = Owner.ShamanTargetHelper.GetTarget(CastingConfig.TargetData);
        if (ReferenceEquals(target, null))
        {
            return false;
        }

        FireballMono fireball = LevelManager.Instance.PoolManager.FireballPool.GetPooledObject();
        fireball.transform.position = Owner.CastPos.transform.position;
        fireball.gameObject.SetActive(true);
        fireball.Fire(Owner, this, target,Config.Speed);
        return true;
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.ShamanTargetHelper.GetTarget(CastingConfig.TargetData);
        return !ReferenceEquals(target, null);
    }
}