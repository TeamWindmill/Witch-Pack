using System;
using System.Collections.Generic;
using Configs;
using Gameplay.Units.Energy_Exp;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;

namespace Gameplay.Units
{
    [Serializable]
    public class ShamanSaveData
    {
        public ShamanConfig Config;
        public ShamanExperienceHandler ShamanExperienceHandler;
        public List<AbilityUpgradeConfig> AbilityUpgrades = new();
        public List<StatMetaUpgradeConfig> StatUpgrades = new();
        public ShamanSaveData(ShamanConfig config)
        {
            Config = config;
            ShamanExperienceHandler = new ShamanExperienceHandler(config.ShamanExperienceConfig);
        }
    }
}