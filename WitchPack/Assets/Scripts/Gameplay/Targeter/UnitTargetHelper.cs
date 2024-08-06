using System;
using System.Collections.Generic;
using Tools.Targeter;

public class UnitTargetHelper<T> where T : BaseUnit
{
    public event Action<BaseUnit> OnTarget;

    public T CurrentTarget { get; private set; }
    public bool IsTaunted { get; private set; }
    private BaseUnit owner;
    private List<T> _availableTargets;
    private List<T> _targetsToAvoid;
    private ITimer _tauntTimer;

    public UnitTargetHelper(Targeter<T> targeter, BaseUnit givenOwner,List<T> targetsToAvoid = null)
    {
        _targetsToAvoid = targetsToAvoid;
        owner = givenOwner;
        _availableTargets = targeter.AvailableTargets;
        owner.Damageable.OnDeathGFX += OnDeath;
    }

    public T GetTarget(TargetData givenData, List<T> targetsToAvoid = null)
    {
        if (IsTaunted) return CurrentTarget;
        
        List<T> totalTargetsToAvoid = null;
        if (targetsToAvoid != null || _targetsToAvoid != null)
        {
            totalTargetsToAvoid = new List<T>();
            if(_targetsToAvoid is { Count: > 0 }) totalTargetsToAvoid.AddRange(_targetsToAvoid);
            if (targetsToAvoid is { Count: > 0 }) totalTargetsToAvoid.AddRange(targetsToAvoid);
        }
        
        var target = TargetingHelper<T>.GetTarget(_availableTargets, givenData, totalTargetsToAvoid, owner.transform);
        if (!ReferenceEquals(target, null))
        {
            if (givenData.AvoidCharmedTargets)
                if (target.Effectable.ContainsStatusEffect(StatusEffectVisual.Charm))
                    return null;

            RemoveCurrentTarget();

            CurrentTarget = target;
            CurrentTarget.Stats[StatType.ThreatLevel].AddModifier(owner.UnitConfig.BaseStats.Threat.value);
            OnTarget?.Invoke(target);
        }

        return target;
    }

    public List<T> GetAvailableTargets(TargetData givenData, List<T> targetsToAvoid = null)
    {
        var validTargets = new List<T>();
        foreach (var target in _availableTargets)
        {
            if (targetsToAvoid != null)
            {
                if (_targetsToAvoid.Contains(target)) continue;
            }
            if(target.IsDead) continue;
            if(givenData.AvoidCharmedTargets && target.Effectable.ContainsStatusEffect(StatusEffectVisual.Charm)) continue;
                
            
            validTargets.Add(target);
        }

        return validTargets;
    }

    public void ApplyTaunt(T target, float duration)
    {
        IsTaunted = true;
        CurrentTarget = target;
        if(_tauntTimer != null) _tauntTimer.RemoveThisTimer();
        _tauntTimer = TimerManager.AddTimer(duration, RemoveTaunt, true);
    }

    private void RemoveTaunt()
    {
        _tauntTimer = null;
        IsTaunted = false;
        CurrentTarget = null;
    }

    public void RemoveCurrentTarget()
    {
        if (!ReferenceEquals(CurrentTarget, null))
        {
            CurrentTarget.Stats[StatType.ThreatLevel].RemoveModifier(owner.UnitConfig.BaseStats.Threat.value);
        }

        CurrentTarget = null;
    }

    public void AddTargetToAvoid(T target)
    {
        _targetsToAvoid ??= new List<T>();
        if(!_targetsToAvoid.Contains(target)) _targetsToAvoid.Add(target);
    }

    public void RemoveTargetToAvoid(T target)
    {
        _targetsToAvoid?.Remove(target);
    }
    private void OnDeath()
    {
        RemoveCurrentTarget();
        owner.Damageable.OnDeathGFX -= OnDeath;
    }
}