using System.Collections.Generic;

public class AoeFire : AoeMono
{
    
    private FireballSO _fireball;
    private Dictionary<Shaman,ITimer> _activeTimers = new();
    public override void Init(BaseUnit owner, CastingAbilitySO abilitySo, float lastingTime,float aoeRange)
    {
        _fireball = abilitySo as FireballSO;
        base.Init(owner, abilitySo, lastingTime,aoeRange);
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
            _activeTimers.Remove(shaman);
        }
    }
    private void OnFireTick(Shaman shaman)
    {
        var damage = new DamageHandler(_fireball.BurnDamage);
        shaman.Damageable.TakeDamage(_owner.DamageDealer,damage,_fireball,false);
    }
}