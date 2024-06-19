using UnityEngine;

public class HealingWeedsMono : RootingVinesMono
{
    private HealingWeeds _healingWeedsAbility;
    public override void Init(BaseUnit owner, CastingAbility ability, float lastingTime, float aoeRange)
    {
        base.Init(owner, ability, lastingTime, aoeRange);
        _healingWeedsAbility = ability as HealingWeeds;
    }

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        enemy.Damageable.OnDeath += HerbalWeeds;
        TimerData<Enemy> timerData = new TimerData<Enemy>(tickTime: 1, data: enemy, tickAmount: _healingWeedsAbility.Config.Root.Duration, usingGameTime: true);
        Timer<Enemy> timer = new Timer<Enemy>(timerData);
        timer.OnTimerEnd += RemoveHerbalWeeds;
        TimerManager.AddTimer(timer);
        enemy.UnitTimers.Add(timer);
    }

    private void RemoveHerbalWeeds(Timer<Enemy> timer)
    {
        timer.Data.Damageable.OnDeath -= HerbalWeeds;
    }

    private void HerbalWeeds(Damageable damageable, DamageDealer damageDealer)
    {
        damageDealer.Owner.Effectable.AddEffects(_healingWeedsAbility.StatusEffects, damageable.Owner.Affector);
        if (damageDealer.Owner is Shaman shaman)
        {
            shaman.ShamanVisualHandler.HealingWeedsEffect.Play();
        }
        SoundManager.Instance.PlayAudioClip(SoundEffectType.HealingWeeds);
    }
}
