using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvyMono : RootingVinesMono
{
    private PoisonIvy poison;
    public override void Init(BaseUnit owner, BaseAbility ability, float lastingTime)
    {
        base.Init(owner, ability, lastingTime);
        poison = ability as PoisonIvy;
    }

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        int numberOfTicks = (int)(poison.PoisonDuration / poison.PoisonTickRate);
        
        TimerData timerData = new TimerData(poison.PoisonTickRate, tickAmount: numberOfTicks, usingGameTime: true);
        DotTimer dotTimer = new DotTimer(timerData, enemy.Damageable.TakeDamage, owner.DamageDealer, poison.PoisonDamage, refAbility, false);
        enemy.UnitVisual.PoisonIvyVisuals.PlayPoisonParticle(poison.PoisonDuration);
        TimerManager.Instance.AddTimer(dotTimer);
    }
}
