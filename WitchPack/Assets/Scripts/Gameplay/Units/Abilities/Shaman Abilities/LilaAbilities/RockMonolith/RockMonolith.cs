
public class RockMonolith : OffensiveAbility
{
    private RockMonolithSO _config;
    private Shaman _shamanOwner;
    private int _damageIncrement;
    public RockMonolith(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        _shamanOwner = Owner as Shaman;
        _config = config as RockMonolithSO;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,_config.Duration));
        //abilityStats.Add(new AbilityStat(AbilityStatType.Size,_config.TauntRadius));
    }

    public override bool CastAbility()
    {
        var targets = Owner.EnemyTargetHelper.GetAvailableTargets(_config.TargetData);
        
        var statusEffects = _shamanOwner.Effectable.AddEffects(StatusEffects,_shamanOwner.Affector);
        statusEffects[0].Ended += _config.TauntState.EndTaunt;

        TimerManager.AddTimer(GetAbilityStatValue(AbilityStatType.Duration), OnTauntEnd);

        _shamanOwner.Damageable.OnHitGFX += IncrementDamage;

        if (targets.Count >= _config.MinEnemiesForTaunt)
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
        Enemy target = Owner.EnemyTargetHelper.GetTarget(_config.TargetData);
        if (!ReferenceEquals(target, null)) //might want to change to multiple enemies near her
        {
            return true;
        }

        return false;
    }

    protected virtual void OnTauntEnd()
    {
        var targets = Owner.EnemyTargetHelper.GetAvailableTargets(_config.TargetData);

        foreach (var target in targets)
        {
            var damageHandler = new DamageHandler(GetAbilityStatValue(AbilityStatType.Damage) + _config.DamageIncreasePerHit * _damageIncrement);
            target.Damageable.TakeDamage(_shamanOwner.DamageDealer,damageHandler,this,false);
        }
        
        _shamanOwner.Damageable.OnHitGFX -= IncrementDamage;
        _damageIncrement = 0;
    }
    
    private void IncrementDamage(bool isCrit)
    {
        _damageIncrement++;
    }
}