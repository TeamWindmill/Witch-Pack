using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class Stat
{
    public event Action OnStatChange;
    public StatType StatType;
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

    public Stat(StatType statType, float baseValue)
    {
        StatType = statType;
        BaseValue = baseValue;
    }

    public void AddModifier(float value)
    {
        _modifiers.Add(value);
        OnStatChange?.Invoke();
    }

    public void RemoveModifier(float value)
    {
        _modifiers.Remove(value);
        OnStatChange?.Invoke();
    }

    public void AddMultiplier(float value)
    {
        _multipliers.Add(value);
        OnStatChange?.Invoke();
    }

    public void RemoveMultiplier(float value)
    {
        _multipliers.Remove(value);
        OnStatChange?.Invoke();
    }
}

public enum StatType
{
    MaxHp,
    BaseDamage, // =basic attack damage
    AttackSpeed, //cdr for auto attacks 
    BaseRange, //range for all attacks 
    MovementSpeed,
    CritDamage,
    CritChance,
    InvincibleTime, //flat duration of invincibility after recieving damage
    AbilityCooldownReduction, //cdr for abilities only (anything that isnt an auto attack)
    Armor, // damage redcutcion from all sources
    HpRegen, //amount of health resotred every second
    BonusStatusEffectDuration, //fixed duration added for every effect applied by unit
    AbilityProjectileSpeed, //if an ability is projectile quicken it by this amount
    AbilityProjectilePenetration, //the amount of times a projectile ability can hit targets before disabling
    Visibility,
    ThreatLevel, //the amount of threat currently on this unit
    Threat, //the amount of threat added to this unit's targets
    EnergyGain,
    
}

