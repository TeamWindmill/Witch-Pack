using System;
using System.Collections.Generic;
using Tools.Targeter;
using UnityEngine;

public class UnitTargetHelper<T> where T : BaseUnit
{
    public event Action<BaseUnit> OnTarget;

    private BaseUnit owner;
    private List<T> _targets;

    public UnitTargetHelper(Targeter<T> targeter, BaseUnit givenOwner)
    {
        owner = givenOwner;
        _targets = targeter.AvailableTargets;
    }

    public T GetTarget(TargetData givenData, List<T> targetsToAvoid = null)
    {
        var target = TargetingHelper<T>.GetTarget(_targets, givenData, targetsToAvoid, owner.transform);
        if (!ReferenceEquals(target, null)) OnTarget?.Invoke(target);
        return target;
    }
}