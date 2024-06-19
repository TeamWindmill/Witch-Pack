public class Aftershock : RockMonolith
{
    public AftershockSO AftershockConfig;
    public Aftershock(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        AftershockConfig = config as AftershockSO;
    }

    protected override void OnTauntEnd()
    {
        
        var aftershockMono = LevelManager.Instance.PoolManager.AftershockPool.GetPooledObject();
        aftershockMono.Activate(_shamanOwner,this,_damageIncrement,true);
        
        _shamanOwner.Damageable.OnHitGFX -= IncrementDamage;
        _damageIncrement = 0;
    }
}