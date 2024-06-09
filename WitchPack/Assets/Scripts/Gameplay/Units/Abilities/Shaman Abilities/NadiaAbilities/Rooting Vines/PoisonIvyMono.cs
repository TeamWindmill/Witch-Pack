
public class PoisonIvyMono : RootingVinesMono
{
    private PoisonIvy poison;
    DamageHandler damage;
    public override void Init(BaseUnit owner, CastingAbility ability, float lastingTime,float aoeRange)
    {
        base.Init(owner, ability, lastingTime,aoeRange);
        poison = ability as PoisonIvy;
    }
    

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        int numberOfTicks = (int)(poison.Config.PoisonDuration / poison.Config.PoisonTickRate);
        
        //TimerData timerData = new TimerData(tickTime : poison.PoisonTickRate, tickAmount: numberOfTicks, usingGameTime: true);
        TimerData<Enemy> timerData = new TimerData<Enemy>(tickTime : poison.Config.PoisonTickRate, enemy, onTimerTick : EnemyTakePoisonDamage, tickAmount: numberOfTicks, usingGameTime: true);
        
        //DotTimer dotTimer = new DotTimer(timerData, enemy.Damageable.TakeDamage, owner.DamageDealer, poison.PoisonDamage, refAbility, false);
        Timer<Enemy> dotTimer = new Timer<Enemy>(timerData);
        dotTimer.OnTimerEnd += StopPoisonParticle;
        TimerManager.AddTimer(dotTimer);
        enemy.UnitTimers.Add(dotTimer);

        enemy.Damageable.OnDeath += RemovePoisonFromEnemyOnDeath;
        enemy.EnemyVisualHandler.PoisonIvyVisuals.PlayPoisonParticle(poison.Config.PoisonDuration);
        SoundManager.Instance.PlayAudioClip(SoundEffectType.PoisonIvy);
    }

    private void RemovePoisonFromEnemyOnDeath(Damageable damageable, DamageDealer damageDealer)
    {
        StopPoisonParticle(damageable.Owner as Enemy);
    }

    private void EnemyTakePoisonDamage(Enemy enemy)
    {
        damage = new DamageHandler(poison.Config.PoisonDamage);
        damage.SetPopupColor(poison.Config.PoisonPopupColor);
        enemy.Damageable.TakeDamage(_owner.DamageDealer, damage, Ability as OffensiveAbility, false);
        
    }

    private void StopPoisonParticle(Timer<Enemy> timer)
    {
        StopPoisonParticle(timer.Data);
    }

    private void StopPoisonParticle(Enemy enemy)
    {
        enemy.EnemyVisualHandler.PoisonIvyVisuals.StopPoisonParticle();
    }


}
