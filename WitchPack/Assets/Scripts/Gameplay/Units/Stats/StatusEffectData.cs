using System;
using System.Collections.Generic;

public readonly struct StatusEffectData
{
    public readonly float Duration
    {
        get
        {
            float value = _duration;
            foreach (var upgrade in Upgrades)
            {
                value += upgrade.DurationAddition;
            }

            return value;
        }
    }
    public readonly float StatValue
    {
        get
        {
            float value = _statValue;
            foreach (var upgrade in Upgrades)
            {
                value += upgrade.ValueAddition;
            }

            return value;
        }
    }
    
    private readonly float _duration;
    private readonly float _statValue;
    public readonly StatusEffectProcess Process;
    public readonly StatType StatTypeAffected;
    public readonly StatusEffectType StatusEffectType;
    public readonly StatusEffectValueType ValueType;
    public readonly bool ShowStatusEffectPopup;
    public readonly List<StatusEffectUpgradeConfig> Upgrades;
    public StatusEffectData(StatusEffectConfig config)
    {
        _duration = config.Duration;
        _statValue = config.Amount;
        Process = config.Process;
        StatTypeAffected = config.StatTypeAffected;
        StatusEffectType = config.StatusEffectType;
        ValueType = config.ValueType;
        ShowStatusEffectPopup = config.ShowStatusEffectPopup;
        Upgrades = new();
    }

    public StatusEffectData(float duration, float statValue, StatusEffectProcess process, StatType statTypeAffected, StatusEffectType statusEffectType, StatusEffectValueType valueType, bool showStatusEffectPopup)
    {
        _duration = duration;
        _statValue = statValue;
        Process = process;
        StatTypeAffected = statTypeAffected;
        StatusEffectType = statusEffectType;
        ValueType = valueType;
        ShowStatusEffectPopup = showStatusEffectPopup;
        Upgrades = new();
    }
}

[Serializable]
public struct StatusEffectUpgradeConfig
{
    public float DurationAddition;
    public float ValueAddition;
    public StatusEffectProcess Process;
    public StatType StatType;
}