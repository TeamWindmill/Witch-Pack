using System;
using System.Collections.Generic;

[Serializable]
public class AbilityStat
{
    public AbilityStatType StatType;
    public float BaseValue { get; }

    public float Value
    {
        get
        {
            var value = BaseValue;
            foreach (var modifier in _modifiers)
            {
                value += modifier;
            }

            float multipliersSum = 0;
            foreach (var multiplier in _multipliers)
            {
                multipliersSum += multiplier;
            }

            if (multipliersSum > 0) return value * multipliersSum;
            return value;
        }
    }

    public int IntValue
    {
        get
        {
            var value = BaseValue;
            foreach (var modifier in _modifiers)
            {
                value += modifier;
            }

            float multipliersSum = 0;
            foreach (var multiplier in _multipliers)
            {
                multipliersSum += multiplier;
            }

            if (multipliersSum > 0) return (int)(value * multipliersSum);
            return (int)value;
        }
    }

    private List<float> _modifiers = new();
    private List<float> _multipliers = new();

    public AbilityStat(AbilityStatType statType, float baseValue)
    {
        StatType = statType;
        BaseValue = baseValue;
    }

    public void AddModifier(float value)
    {
        _modifiers.Add(value);
    }

    public void RemoveModifier(float value)
    {
        _modifiers.Remove(value);
    }

    public void AddMultiplier(float value)
    {
        _multipliers.Add(value);
    }

    public void RemoveMultiplier(float value)
    {
        _multipliers.Remove(value);
    }
}

public enum AbilityStatType
{
    Damage,
    Cooldown,
    Speed,
    Range, //not working currently
    CastTime,
    Penetration,
    ExtraPenetrationPerKill,
    KillToIncreasePenetration,
    EnergyPointsOnKill,
    ProjectilesAmount,
    LifeTime,
    BounceAmount,
    
}
