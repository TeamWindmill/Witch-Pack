using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWeedsMono : RootingVinesMono
{
    [SerializeField] private StatusEffectConfig speedBoost;
    [SerializeField] private StatusEffectConfig regenBoost;
    [SerializeField] private StatusEffectConfig root;

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        enemy.Damageable.OnDeath += HerbalWeeds;
        TimerData<Enemy> timerData = new TimerData<Enemy>(tickTime: 1, data: enemy, tickAmount: root.Duration, usingGameTime: true);
        Timer<Enemy> timer = new Timer<Enemy>(timerData);
        timer.OnTimerEnd += RemoveHerbalWeeds;
        TimerManager.Instance.AddTimer<Enemy>(timer);
    }

    private void RemoveHerbalWeeds(Timer<Enemy> timer)
    {
        timer.Data.Damageable.OnDeath -= HerbalWeeds;
    }

    private void HerbalWeeds(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability)
    {
        damageDealer.Owner.Effectable.AddEffect(speedBoost, damageable.Owner.Affector);
        damageDealer.Owner.Effectable.AddEffect(regenBoost, damageable.Owner.Affector);
    }
}
