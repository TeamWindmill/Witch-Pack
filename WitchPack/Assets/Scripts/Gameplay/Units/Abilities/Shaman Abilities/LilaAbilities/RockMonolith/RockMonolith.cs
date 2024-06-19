
public class RockMonolith : OffensiveAbility
{
    public RockMonolithSO RockMonolithConfig;
    protected Shaman _shamanOwner;
    protected int _damageIncrement;
    public RockMonolith(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        _shamanOwner = Owner as Shaman;
        RockMonolithConfig = config as RockMonolithSO;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,RockMonolithConfig.Duration));
        //abilityStats.Add(new AbilityStat(AbilityStatType.Size,_config.TauntRadius));
    }

    public override bool CastAbility()
    {
        var targets = Owner.EnemyTargetHelper.GetAvailableTargets(RockMonolithConfig.TargetData);
        
        var statusEffects = _shamanOwner.Effectable.AddEffects(StatusEffects,_shamanOwner.Affector);
        statusEffects[0].Ended += RockMonolithConfig.TauntState.EndTaunt;

        TimerManager.AddTimer(GetAbilityStatValue(AbilityStatType.Duration), OnTauntEnd);

        _shamanOwner.Damageable.OnHitGFX += IncrementDamage;

        if (targets.Count >= RockMonolithConfig.MinEnemiesForTaunt)
        {
            foreach (var target in targets)
            {
                target.ShamanTargetHelper.ApplyTaunt(_shamanOwner, GetAbilityStatValue(AbilityStatType.Duration));
                target.EnemyAI.SetState(typeof(Taunt));
            }

            return true;
        }
        
        return false;
    }

    public override bool CheckCastAvailable()
    {
        Enemy target = Owner.EnemyTargetHelper.GetTarget(RockMonolithConfig.TargetData);
        if (!ReferenceEquals(target, null)) //might want to change to multiple enemies near her
        {
            return true;
        }

        return false;
    }

    protected virtual void OnTauntEnd()
    {
        //activate aftershockMono
        var aftershockMono = LevelManager.Instance.PoolManager.AftershockPool.GetPooledObject();
        aftershockMono.Activate(_shamanOwner,this,_damageIncrement,false);
        
        _shamanOwner.Damageable.OnHitGFX -= IncrementDamage;
        _damageIncrement = 0;
    }
    
    protected void IncrementDamage(bool isCrit)
    {
        _damageIncrement++;
    }
}