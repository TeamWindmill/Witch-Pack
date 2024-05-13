using System;
using System.Collections.Generic;

[Serializable]
public abstract class AbilityStat<TValue> where TValue : struct
{
    public AbilityStatType StatType;
    protected List<TValue> _modifiers;

    public TValue StatValue => GetStatValue();

    public void AddModifier(TValue value)
    {
        _modifiers.Add(value);
    }
    public void RemoveModifier(TValue value)
    {
        _modifiers.Remove(value);
    }

    protected abstract TValue GetStatValue();
}

public enum AbilityStatType
{
    Damage,
    Cooldown,
    Speed,
    Range,
    CastTime,
    
}
