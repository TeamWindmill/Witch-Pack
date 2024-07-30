using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;

namespace Gameplay.Units.Stats
{
    public readonly struct StatusEffectData
    {
        public readonly StatusEffectStat Duration;
        public readonly List<Stat> Values;
        public readonly StatusEffectProcess Process;
        public readonly StatusEffectVisual StatusEffectVisual;
        public readonly bool ShowStatusEffectPopup;
        public readonly StatUpgrade[] StatUpgrades;

        public StatusEffectData(StatusEffectConfig config)
        {
            Values = new List<Stat>();
            foreach (var statUpgrade in config.StatUpgrades)
            {
                Values.Add(new Stat(statUpgrade.StatType, statUpgrade.StatValue));
            }
            Duration = new StatusEffectStat(StatusEffectStatType.Duration, config.Duration);

            Process = config.Process;
            StatusEffectVisual = config.StatusEffectVisual;
            ShowStatusEffectPopup = config.ShowStatusEffectPopup;
            StatUpgrades = config.StatUpgrades;
        }

        public void AddUpgrade(StatusEffectUpgradeConfig upgradeConfig)
        {
            Duration.AddUpgrade(upgradeConfig.Duration);
            foreach (var configValue in upgradeConfig.StatUpgrades)
            {
                foreach (var stat in Values)
                {
                    if (stat.StatType == configValue.StatType)
                    {
                        stat.AddStatValue(configValue.Factor, configValue.StatValue);
                    }
                }
            }
        }
    }

    [Serializable]
    public class StatusEffectUpgradeConfig
    {
        public StatusEffectStatUpgradeConfig Duration = new(StatusEffectStatType.Duration);
        public StatUpgrade[] StatUpgrades;
        public StatusEffectProcess Process;
        public StatusEffectVisual StatusEffectVisual;
        public StatType StatType;
    }

    public class StatusEffectStat : BaseStat<StatusEffectStatType>
    {
        public StatusEffectStat(StatusEffectStatType statType, float baseValue) : base(statType, baseValue)
        {
        }

        public void AddUpgrade(StatusEffectStatUpgradeConfig statUpgradeConfig)
        {
            AddStatValue(statUpgradeConfig.Factor, statUpgradeConfig.StatValue);
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
    }
}