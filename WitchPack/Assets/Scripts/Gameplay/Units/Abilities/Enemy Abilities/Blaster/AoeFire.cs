using System.Collections.Generic;
using Systems.Pool_System;
using UnityEngine;

public class AoeFire : AoeMono , IPoolable
{
    
    private Fireball _fireball;
    private Dictionary<Shaman,ITimer> _activeTimers = new();
    public override void Init(BaseUnit owner, CastingAbility ability, float lastingTime,float aoeRange)
    {
        _fireball = ability as Fireball;
        base.Init(owner, ability, lastingTime,aoeRange);
    }

    protected override void OnShamanEnter(Shaman shaman)
    {
        if (!_activeTimers.ContainsKey(shaman))
        {
            var timer = TimerManager.AddTimer(_fireball.Config.TickTime, shaman, OnFireTick, true, _fireball.Config.TickAmount);
            _activeTimers.Add(shaman,timer);
        }
    }
    protected override void OnShamanExit(Shaman shaman)
    {
        if (_activeTimers.TryGetValue(shaman,out var timer))
        {
            timer.RemoveThisTimer();
            _activeTimers.Remove(shaman);
        }
    }
    private void OnFireTick(Shaman shaman)
    {
        var damage = new DamageHandler(_fireball.Config.BurnDamage);
        shaman.Damageable.TakeDamage(_owner.DamageDealer,damage,_fireball,false);
    }

    public GameObject PoolableGameObject => gameObject;
}