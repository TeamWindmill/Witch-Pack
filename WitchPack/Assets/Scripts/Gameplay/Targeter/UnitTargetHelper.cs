using System;
using System.Collections.Generic;
using Tools.Targeter;
using UnityEngine;

public class UnitTargetHelper<T> where T : BaseUnit
{
    public event Action<BaseUnit> OnTarget;

    public T CurrentTarget { get; private set; }
    private BaseUnit owner;
    private List<T> _targets;
    private List<T> _targetsToAvoid;

    public UnitTargetHelper(Targeter<T> targeter, BaseUnit givenOwner,List<T> targetsToAvoid = null)
    {
        _targetsToAvoid = targetsToAvoid;
        owner = givenOwner;
        _targets = targeter.AvailableTargets;
        owner.Damageable.OnDeathGFX += OnDeath;
    }

    public T GetTarget(TargetData givenData, List<T> targetsToAvoid = null)
    {
        List<T> totalTargetsToAvoid = null;
        if (targetsToAvoid != null || _targetsToAvoid != null)
        {
            totalTargetsToAvoid = new List<T>();
            if(_targetsToAvoid is { Count: > 0 }) totalTargetsToAvoid.AddRange(_targetsToAvoid);
            if (targetsToAvoid is { Count: > 0 }) totalTargetsToAvoid.AddRange(targetsToAvoid);
        }
        
        var target = TargetingHelper<T>.GetTarget(_targets, givenData, totalTargetsToAvoid, owner.transform);
        if (!ReferenceEquals(target, null))
        {
            if (givenData.AvoidCharmedTargets)
                if (target.Effectable.ContainsStatusEffect(StatusEffectType.Charm))
                    return null;

            if (!ReferenceEquals(CurrentTarget, null))
            {
                CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, -owner.UnitConfig.BaseStats.Threat.value);
            }

            CurrentTarget = target;
            CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, owner.UnitConfig.BaseStats.Threat.value);
            OnTarget?.Invoke(target);
        }

        return target;
    }
    private void OnDeath()
    {
        if (!ReferenceEquals(CurrentTarget, null))
        {
            CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, -owner.UnitConfig.BaseStats.Threat.value);
        }
        owner.Damageable.OnDeathGFX -= OnDeath;
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
}