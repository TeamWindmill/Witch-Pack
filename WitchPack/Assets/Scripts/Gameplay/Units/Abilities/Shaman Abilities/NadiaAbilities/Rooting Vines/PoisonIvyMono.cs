
public class PoisonIvyMono : RootingVinesMono
{
    private PoisonIvy poisonIvy;
    DamageHandler damage;
    private Timer<Enemy> dotTimer;
    public override void Init(BaseUnit owner, CastingAbility ability, float lastingTime,float aoeRange)
    {
        base.Init(owner, ability, lastingTime,aoeRange);
        poisonIvy = ability as PoisonIvy;
    }
    

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        int numberOfTicks = (int)(poisonIvy.Config.PoisonDuration / poisonIvy.Config.PoisonTickRate);
        
        //TimerData timerData = new TimerData(tickTime : poison.PoisonTickRate, tickAmount: numberOfTicks, usingGameTime: true);
        TimerData<Enemy> timerData = new TimerData<Enemy>(tickTime : poisonIvy.Config.PoisonTickRate, enemy, onTimerTick : EnemyTakePoisonDamage, tickAmount: numberOfTicks, usingGameTime: true);
        
        //DotTimer dotTimer = new DotTimer(timerData, enemy.Damageable.TakeDamage, owner.DamageDealer, poison.PoisonDamage, refAbility, false);
        dotTimer = new Timer<Enemy>(timerData);
        dotTimer.OnTimerEnd += StopPoisonParticle;
        TimerManager.AddTimer(dotTimer);
        enemy.UnitTimers.Add(dotTimer);

        enemy.Damageable.OnDeath += RemovePoisonFromEnemyOnDeath;
        enemy.EnemyVisualHandler.EnemyEffectHandler.PoisonIvyVisuals.PlayPoisonParticle(poisonIvy.Config.PoisonDuration);
        SoundManager.PlayAudioClip(SoundEffectType.PoisonIvy);
    }

    private void RemovePoisonFromEnemyOnDeath(Damageable damageable, DamageDealer damageDealer)
    {
        if(!ReferenceEquals(dotTimer,null)) dotTimer.RemoveThisTimer();
        StopPoisonParticle(damageable.Owner as Enemy);
        damageable.OnDeath -= RemovePoisonFromEnemyOnDeath;
    }

    private void EnemyTakePoisonDamage(Enemy enemy)
    {
        damage = new DamageHandler(poisonIvy.GetAbilityStatValue(AbilityStatType.DotDamage));
        damage.SetPopupColor(poisonIvy.Config.PoisonPopupColor);
        enemy.Damageable.TakeDamage(_owner.DamageDealer, damage, Ability as OffensiveAbility, false);
        
    }

    private void StopPoisonParticle(Timer<Enemy> timer)
    {
        StopPoisonParticle(timer.Data);
    }

    private void StopPoisonParticle(Enemy enemy)
    {
        enemy.EnemyVisualHandler.EnemyEffectHandler.PoisonIvyVisuals.StopPoisonParticle();
    }


}
