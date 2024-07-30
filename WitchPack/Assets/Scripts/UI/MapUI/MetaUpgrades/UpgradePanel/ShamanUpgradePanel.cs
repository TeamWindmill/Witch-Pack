using System;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units;
using Sirenix.Utilities;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;
using UI.UISystem;
using UnityEngine;

namespace UI.MapUI.MetaUpgrades.UpgradePanel
{
    public class ShamanUpgradePanel : UIElement
    {
        public event Action<StatMetaUpgradeConfig> OnStatUpgrade;
        [SerializeField] private AbilityMetaUpgrade[] _abilityMetaUpgrades;
        [SerializeField] private StatMetaUpgrade _statMetaUpgrades;

        private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
        public ShamanSaveData ShamanSaveData { get; private set; }

        public void Init(ShamanSaveData shamanSaveData)
        {
            Hide();
            ShamanSaveData = shamanSaveData;
            _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
            for (int i = 0; i < _abilityMetaUpgrades.Length; i++)
            {
                _abilityMetaUpgrades[i].Init(i, _shamanMetaUpgradeConfig.AbilityPanelUpgrades[i]);
            }
        
            _statMetaUpgrades.Init(_shamanMetaUpgradeConfig.StatPanelUpgrades.StatUpgrades);
            //SelectAbility(0,shamanSaveData.Config.RootAbilities[0]);
            Show();
        }

        public override void Refresh()
        {
            _abilityMetaUpgrades.ForEach(upgrade => upgrade.Refresh());
            _statMetaUpgrades.Refresh();
        }

        public override void Hide()
        {
            ShamanSaveData = null;
            base.Hide();
        }

        public void SelectAbility(int abilityPanelIndex)
        {
            (WindowManager as UpgradeWindow).SelectAbility(abilityPanelIndex, ShamanSaveData.Config.RootAbilities[abilityPanelIndex]);
        }

        public void SelectAbility(int abilityPanelIndex ,AbilitySO ability)
        {
            //deselect all
            _abilityMetaUpgrades.ForEach(upgrade => upgrade.SelectAbility(false));
            _statMetaUpgrades.SelectAbility(false);
        
            //select
            if (!ability)
            {
                switch (abilityPanelIndex)
                {
                    case < 2:
                        _abilityMetaUpgrades[abilityPanelIndex].SelectAbility(true);
                        //(WindowManager as UpgradeWindow)?.SelectAbility(_abilityMetaUpgrades[abilityPanelIndex].AbilityPanelConfig.Ability);
                        break;
                    case 2:
                        _statMetaUpgrades.SelectAbility(true);
                        //(WindowManager as UpgradeWindow)?.SelectAbility(ShamanSaveData.Config.RootAbilities[2]);
                        break;
                }
            }
            else
            {
                switch (abilityPanelIndex)
                {
                    case < 2:
                        _abilityMetaUpgrades[abilityPanelIndex].SelectAbility(true);
                        break;
                    case 2:
                        _statMetaUpgrades.SelectAbility(true);
                        break;
                }
                //(WindowManager as UpgradeWindow)?.SelectAbility(ability);
            }
        }

        public void AddUpgradeToShaman(AbilityUpgradeConfig abilityUpgrade)
        {
            ShamanSaveData.AbilityUpgrades.Add(abilityUpgrade);
            UpgradeShaman(abilityUpgrade.SkillPointsCost);

        }
        public void AddUpgradeToShaman(StatMetaUpgradeConfig statMetaUpgrade)
        {
            ShamanSaveData.StatUpgrades.Add(statMetaUpgrade);
            UpgradeShaman(statMetaUpgrade.SkillPointsCost);
            OnStatUpgrade?.Invoke(statMetaUpgrade);
        }
        public static bool CheckIfUpgradeAvailable(ShamanSaveData shamanSaveData)
        {
            foreach (var abilityPanelUpgrade in shamanSaveData.Config.ShamanMetaUpgradeConfig.AbilityPanelUpgrades)
            {
                foreach (var abilityUpgrade in abilityPanelUpgrade.StatUpgrades)
                {
                    if(shamanSaveData.AbilityUpgrades.Contains(abilityUpgrade)) continue;
                    if(abilityUpgrade.SkillPointsCost <= shamanSaveData.ShamanExperienceHandler.AvailableSkillPoints) return true;
                }
            }

            foreach (var statUpgrade in shamanSaveData.Config.ShamanMetaUpgradeConfig.StatPanelUpgrades.StatUpgrades)
            {
                if(shamanSaveData.StatUpgrades.Contains(statUpgrade)) continue;
                if(statUpgrade.SkillPointsCost <= shamanSaveData.ShamanExperienceHandler.AvailableSkillPoints) return true;
            }

            return false;
        }

        private void UpgradeShaman(int skillPointCost)
        {
            ShamanSaveData.ShamanExperienceHandler.UseSkillPoints(skillPointCost);
            WindowManager.Refresh();
        }
    }
}