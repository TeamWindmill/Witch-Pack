
public class PoisonIvyMono : RootingVinesMono
{
    private PoisonIvySO poison;
    DamageHandler damage;
    public override void Init(BaseUnit owner, CastingAbilitySO abilitySo, float lastingTime,float aoeRange)
    {
        base.Init(owner, abilitySo, lastingTime,aoeRange);
        poison = abilitySo as PoisonIvySO;
    }
    

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        int numberOfTicks = (int)(poison.PoisonDuration / poison.PoisonTickRate);
        
        //TimerData timerData = new TimerData(tickTime : poison.PoisonTickRate, tickAmount: numberOfTicks, usingGameTime: true);
        TimerData<Enemy> timerData = new TimerData<Enemy>(tickTime : poison.PoisonTickRate, enemy, onTimerTick : EnemyTakePoisonDamage, tickAmount: numberOfTicks, usingGameTime: true);
        
        //DotTimer dotTimer = new DotTimer(timerData, enemy.Damageable.TakeDamage, owner.DamageDealer, poison.PoisonDamage, refAbility, false);
        Timer<Enemy> dotTimer = new Timer<Enemy>(timerData);
        dotTimer.OnTimerEnd += StopPoisonParticle;
        TimerManager.Instance.AddTimer(dotTimer);
        enemy.UnitTimers.Add(dotTimer);

        enemy.Damageable.OnDeath += RemovePoisonFromEnemyOnDeath;
        enemy.EnemyVisualHandler.PoisonIvyVisuals.PlayPoisonParticle(poison.PoisonDuration);
        SoundManager.Instance.PlayAudioClip(SoundEffectType.PoisonIvy);
    }

    private void RemovePoisonFromEnemyOnDeath(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbilitySO abilitySo)
    {
        StopPoisonParticle(damageable.Owner as Enemy);
    }

    private void EnemyTakePoisonDamage(Enemy enemy)
    {
        damage = new DamageHandler(poison.PoisonDamage);
        damage.SetPopupColor(poison.PoisonPopupColor);
        enemy.Damageable.TakeDamage(_owner.DamageDealer, damage, AbilitySo as OffensiveAbilitySO, false);
        
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
