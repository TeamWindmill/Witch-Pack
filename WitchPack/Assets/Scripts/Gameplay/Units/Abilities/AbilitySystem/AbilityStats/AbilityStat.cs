using System;
using System.Collections.Generic;

[Serializable]
public class AbilityStat
{
    public AbilityStatType StatType;
    public float BaseStatValue;
    
    private List<float> _modifiers;

    public AbilityStat(AbilityStatType statType, float baseStatValue)
    {
        StatType = statType;
        BaseStatValue = baseStatValue;
    }

    public void AddModifier(float value)
    {
        _modifiers.Add(value);
    }
    public void RemoveModifier(float value)
    {
        _modifiers.Remove(value);
    }
    public float GetStatValue()
    {
        var value = BaseStatValue;
        foreach (var modifier in _modifiers)
        {
            value += modifier;
        }

        return value;
    }
}

public enum AbilityStatType
{
    Damage,
    Cooldown,
    Speed,
    Range,
    CastTime,
    penetration,
}
