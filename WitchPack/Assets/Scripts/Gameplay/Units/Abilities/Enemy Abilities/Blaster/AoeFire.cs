using System.Collections.Generic;
using UnityEngine;

public class AoeFire : AoeMono
{
    
    private FireballSO _fireball;
    private Dictionary<Shaman,ITimer> _activeTimers = new();
    public override void Init(BaseUnit owner, CastingAbility ability, float lastingTime,float aoeRange)
    {
        _fireball = ability as FireballSO;
        base.Init(owner, ability, lastingTime,aoeRange);
    }

    protected override void OnShamanEnter(Shaman shaman)
    {
        if (!_activeTimers.ContainsKey(shaman))
        {
            var timer = TimerManager.Instance.AddTimer(_fireball.TickTime, shaman, OnFireTick, true, _fireball.TickAmount);
            _activeTimers.Add(shaman,timer);
        }
    }
    protected override void OnShamanExit(Shaman shaman)
    {
        if (_activeTimers.TryGetValue(shaman,out var timer))
        {
            timer.RemoveThisTimer();
        }
    }
    private void OnFireTick(Shaman shaman)
    {
        var damage = new DamageHandler(_fireball.BurnDamage);
        damage.SetPopupColor(_fireball.BurnPopupColor);
        shaman.Damageable.TakeDamage(_owner.DamageDealer,damage,_fireball,false);
    }
}