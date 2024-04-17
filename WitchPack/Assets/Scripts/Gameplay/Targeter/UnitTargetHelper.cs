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

    public UnitTargetHelper(Targeter<T> targeter, BaseUnit givenOwner)
    {
        owner = givenOwner;
        _targets = targeter.AvailableTargets;
        owner.Damageable.OnDeathGFX += OnDeath;
    }

    private void OnDeath()
    {
        if (!ReferenceEquals(CurrentTarget, null))
        {
            CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel, -owner.UnitConfig.BaseStats.Threat.value);
        }
        owner.Damageable.OnDeathGFX -= OnDeath;
    }

    public T GetTarget(TargetData givenData, List<T> targetsToAvoid = null)
    {
        var target = TargetingHelper<T>.GetTarget(_targets, givenData, targetsToAvoid, owner.transform);
        if (!ReferenceEquals(target, null))
        {
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
}