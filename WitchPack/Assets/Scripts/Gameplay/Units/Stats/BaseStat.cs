using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseStat<T> //float/int type basic stat
{
    public event Action<float> OnStatChange;
    public T StatType;
    public float BaseValue { get; }

    private List<float> _modifiers = new();
    private List<float> _multipliers = new();
    private List<float> _dividers = new();
    
    private (bool,float) _overrideValue;

    public BaseStat(T statType, float baseValue)
    {
        StatType = statType;
        BaseValue = baseValue;
    }
    public int IntValue => (int)Value;
    public float Value
    {
        get
        {
            if (_overrideValue.Item1) return _overrideValue.Item2;
            
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

            if (multipliersSum < 0)
            {
                Debug.LogError($"{StatType} Multipliers are smaller than 0");
                return value;
            }
            
            if (_dividers.Sum() == 0) return value * multipliersSum;

            float dividerSum = 1;
            foreach (var divider in _dividers)
            {
                dividerSum -= divider;
            }

            if (dividerSum < 0) dividerSum = 0;
            
            return value * multipliersSum * dividerSum;
        }
    }

    public void AddStatValue(Factor factor, float value)
    {
        switch (factor)
        {
            case Factor.Add:
                AddModifier(value);
                break;
            case Factor.Subtract:
                AddModifier(-value);
                break;
            case Factor.Multiply:
                AddMultiplier(value);
                break;
            case Factor.Divide:
                AddDivider(value);
                break;
            case Factor.OverrideValue:
                AddOverrideToValue(value);
                break;
        }
    }
    public void RemoveStatValue(Factor factor, float value)
    {
        switch (factor)
        {
            case Factor.Add:
                RemoveModifier(value);
                break;
            case Factor.Subtract:
                RemoveModifier(-value);
                break;
            case Factor.Multiply:
                RemoveMultiplier(value);
                break;
            case Factor.Divide:
                RemoveDivider(value);
                break;
            case Factor.OverrideValue:
                RemoveOverrideToValue();
                break;
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
    
    public void AddDivider(float value)
    {
        _dividers.Add(value);
        OnStatChange?.Invoke(Value);
    }

    public void RemoveDivider(float value)
    {
        _dividers.Remove(value);
        OnStatChange?.Invoke(Value);
    }
    public void AddOverrideToValue(float value)
    {
        _overrideValue.Item2 = value;
        _overrideValue.Item1 = true;
        OnStatChange?.Invoke(Value);
    }

    public void RemoveOverrideToValue()
    {
        _overrideValue.Item1 = false;
        OnStatChange?.Invoke(Value);
    }
}


public enum Factor
{
    Add,
    Subtract,
    Multiply,
    Divide,
    None,
    OverrideValue,
}