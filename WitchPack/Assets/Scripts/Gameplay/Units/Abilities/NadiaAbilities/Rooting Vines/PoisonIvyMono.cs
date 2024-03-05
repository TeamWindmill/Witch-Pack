using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Units.Abilities;
using UnityEngine;

public class PoisonIvyMono : RootingVinesMono
{
    private PoisonIvy poison;

    DamageHandler damage;
    public override void Init(BaseUnit owner, OffensiveAbility ability, float lastingTime)
    {
        base.Init(owner, ability, lastingTime);
        poison = ability as PoisonIvy;
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
        enemy.UnitVisual.PoisonIvyVisuals.PlayPoisonParticle(poison.PoisonDuration);
    }

    private void RemovePoisonFromEnemyOnDeath(Damageable damageable, DamageDealer damageDealer, DamageHandler damage, BaseAbility ability)
    {
        StopPoisonParticle(damageable.Owner as Enemy);
    }

    private void EnemyTakePoisonDamage(Enemy enemy)
    {
        damage = new DamageHandler(poison.PoisonDamage);
        enemy.Damageable.TakeDamage(owner.DamageDealer, damage, refAbility, false);
    }

    private void StopPoisonParticle(Timer<Enemy> timer)
    {
        StopPoisonParticle(timer.Data);
    }

    private void StopPoisonParticle(Enemy enemy)
    {
        enemy.UnitVisual.PoisonIvyVisuals.StopPoisonParticle();
    }


}
