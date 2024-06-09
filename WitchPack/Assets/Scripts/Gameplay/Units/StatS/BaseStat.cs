using System;
using System.Collections.Generic;
using System.Linq;

public abstract class BaseStat<T>
{
    public event Action<float> OnStatChange;
    public T StatType;
    public float BaseValue { get; }

    private List<float> _modifiers = new();
    private List<float> _multipliers = new();

    public BaseStat(T statType, float baseValue)
    {
        StatType = statType;
        BaseValue = baseValue;
    }

    public float Value
    {
        get
        {
            var value = BaseValue;
            foreach (var modifier in _modifiers)
            {
                value += modifier;
            }

            if (_multipliers.Sum() == 0) return value;

            float multipliersSum = 1;
            foreach (var multiplier in _multipliers)
            {
                multipliersSum += multiplier;
            }

            return value * multipliersSum;
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

            if (_multipliers.Sum() == 0) return (int)value;

            float multipliersSum = 1;
            foreach (var multiplier in _multipliers)
            {
                multipliersSum += multiplier;
            }

            return (int)(value * multipliersSum);
        }
    }

    public void AddModifier(float value)
    {
        _modifiers.Add(value);
        OnStatChange?.Invoke(Value);
    }

    public void RemoveModifier(float value)
    {
        _modifiers.Remove(value);
        OnStatChange?.Invoke(Value);
    }

    public void AddMultiplier(float value)
    {
        _multipliers.Add(value);
        OnStatChange?.Invoke(Value);
    }

    public void RemoveMultiplier(float value)
    {
        _multipliers.Remove(value);
        OnStatChange?.Invoke(Value);
    }
}