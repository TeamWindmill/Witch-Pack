using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public readonly struct StatusEffectData
{
    public readonly StatusEffectStat Duration;
    public readonly StatusEffectStat StatValue;
    public readonly StatusEffectProcess Process;
    public readonly StatType StatTypeAffected;
    public readonly StatusEffectType StatusEffectType;
    public readonly Factor Factor;
    public readonly bool ShowStatusEffectPopup;
    public StatusEffectData(StatusEffectConfig config)
    {
        Duration = new StatusEffectStat(StatusEffectStatType.Duration,config.Duration);
        StatValue = new StatusEffectStat(StatusEffectStatType.Value,config.Amount);
        Process = config.Process;
        StatTypeAffected = config.StatTypeAffected;
        StatusEffectType = config.StatusEffectType;
        ShowStatusEffectPopup = config.ShowStatusEffectPopup;
        Factor = config.Factor;
    }

    public StatusEffectData(float duration, float statValue, StatusEffectProcess process, StatType statTypeAffected, StatusEffectType statusEffectType, Factor factor, bool showStatusEffectPopup)
    {
        Duration = new StatusEffectStat(StatusEffectStatType.Duration,duration);
        StatValue = new StatusEffectStat(StatusEffectStatType.Value,statValue);
        Process = process;
        StatTypeAffected = statTypeAffected;
        StatusEffectType = statusEffectType;
        ShowStatusEffectPopup = showStatusEffectPopup;
        Factor = factor;
    }

    public void AddUpgrade(StatusEffectUpgradeConfig upgradeConfig)
    {
        Duration.AddUpgrade(upgradeConfig.Duration);
        StatValue.AddUpgrade(upgradeConfig.Value);
    }
}

[Serializable]
public class StatusEffectUpgradeConfig
{
    public StatusEffectStatUpgradeConfig Duration = new (StatusEffectStatType.Duration);
    public StatusEffectStatUpgradeConfig Value = new (StatusEffectStatType.Value);
    public StatusEffectProcess Process;
    public StatType StatType;
}

public class StatusEffectStat : BaseStat<StatusEffectStatType>
{
    public StatusEffectStat(StatusEffectStatType statType, float baseValue) : base(statType, baseValue)
    {
    }

    public void AddUpgrade(StatusEffectStatUpgradeConfig statUpgradeConfig)
    {
        AddStatValue(statUpgradeConfig.Factor,statUpgradeConfig.StatValue);
    }
}

[Serializable]
public struct StatusEffectStatUpgradeConfig
{
    [ReadOnly] public StatusEffectStatType StatType;
    public float StatValue;
    public Factor Factor;

    public StatusEffectStatUpgradeConfig(StatusEffectStatType statType) : this() => StatType = statType;
}

public enum StatusEffectStatType
{
    Duration,
    Value,
}