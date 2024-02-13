using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvyMono : RootingVinesMono
{
    [SerializeField] private float poisonDuration;
    [SerializeField] private float poisonTickRate;
    [SerializeField] private int poisonDamage;
    private DamageHandler damageHandler;
    private Enemy lastEnemy;

    public override void Init(BaseUnit owner, BaseAbility ability)
    {
        base.Init(owner, ability);
        damageHandler = new DamageHandler(poisonDamage);
    }

    protected override void OnRoot(Enemy enemy)
    {
        base.OnRoot(enemy);
        lastEnemy = enemy;
        int numberOfTicks = (int)(poisonDuration / poisonTickRate);
        //enemy.Damageable.TakeDamageOverTime(owner.DamageDealer, damageHandler, refAbility, false, numberOfTicks, poisonTickRate);
        //StartCoroutine(enemy.Damageable.TakeDamageOverTime(owner.DamageDealer, damageHandler, refAbility, false, poisonDuration, poisonTickRate));
        TimerData timerData = new TimerData(poisonTickRate, tickAmount: numberOfTicks, usingGameTime: true);
        DotTimer dotTimer = new DotTimer(timerData, enemy.Damageable.TakeDamage, owner.DamageDealer, damageHandler, refAbility, false);

        GAME_TIME.AddTimer(dotTimer);
    }
}
