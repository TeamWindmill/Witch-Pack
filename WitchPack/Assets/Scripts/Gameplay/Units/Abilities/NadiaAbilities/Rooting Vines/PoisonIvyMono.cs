using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvyMono : RootingVinesMono
{
    [SerializeField] private float poisonDuration;
    [SerializeField] private float poisonTickRate;
    [SerializeField] private int poisonDamage;
    private DamageHandler damageHandler;

    public override void Init(BaseUnit owner, BaseAbility ability)
    {
        base.Init(owner, ability);
        damageHandler = new DamageHandler(poisonDamage);
    }

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        int numberOfTicks = (int)(poisonDuration / poisonTickRate);
        
        TimerData timerData = new TimerData(poisonTickRate, tickAmount: numberOfTicks, usingGameTime: true);
        DotTimer dotTimer = new DotTimer(timerData, enemy.Damageable.TakeDamage, owner.DamageDealer, damageHandler, refAbility, false);
        //enemy.UnitVisual.Pois
        TimerManager.Instance.AddTimer(dotTimer);
    }
}
