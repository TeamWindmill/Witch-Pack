using System.Collections.Generic;
using UnityEngine;

public class UpgradeWindow : UIWindowManager
{
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;
    [SerializeField] private ShamanDetailsPanel _shamanDetailsPanel;
    [SerializeField] private AbilityDetailsPanel _abilityDetailsPanel;
    [SerializeField] private UpgradesPartyUIPanel _upgradesPartyUIPanel;
    [SerializeField] private List<ShamanSaveData> _shamanRoster;

    public ShamanSaveData SelectedShaman { get; private set; }
    public override void Show()
    {
        _shamanRoster = GameManager.SaveData.ShamanRoster;
        _upgradesPartyUIPanel.Init();
        _shamanUpgradePanel.OnStatUpgrade += _shamanDetailsPanel.AddUpgradeToStats;
        base.Show();
        SelectShaman(_shamanRoster[0]);
    }

    public void SelectShaman(ShamanSaveData shamanSaveData)
    {
        SelectedShaman = shamanSaveData;
        _upgradesPartyUIPanel.SelectShamanIcon(shamanSaveData);
        _shamanUpgradePanel.Init(shamanSaveData);
        _shamanDetailsPanel.Init(shamanSaveData);
        SelectAbility(0,SelectedShaman.Config.RootAbilities[0]);
        Refresh();
    }

    #region Select Ability

    public void SelectAbility(int abilityPanelIndex,AbilitySO abilitySo)
    {
        _abilityDetailsPanel.Init(SelectedShaman,abilitySo,null);
        _shamanUpgradePanel.SelectAbility(abilityPanelIndex,abilitySo);
        Refresh();
    }
    public void SelectAbility(int abilityPanelIndex,AbilitySO abilitySo, AbilityUpgradeConfig upgradeConfig)
    {
        _abilityDetailsPanel.Init(SelectedShaman,abilitySo,upgradeConfig?.AbilitiesToUpgrade);
        _shamanUpgradePanel.SelectAbility(abilityPanelIndex,abilitySo);
        foreach (var statUpgrade in upgradeConfig.Stats)
        {
            _abilityDetailsPanel.ShowStatBonus(statUpgrade.StatType,statUpgrade.Factor,statUpgrade.StatValue);
        }
        Refresh();
    }

    public void SelectAbility(int abilityPanelIndex, AbilitySO abilitySo, StatMetaUpgradeConfig upgradeConfig)
    {
        foreach (var statUpgrade in upgradeConfig.Stats)
        {
            _shamanDetailsPanel.ShowStatBonus(statUpgrade.StatType,statUpgrade.Factor,statUpgrade.StatValue);
        }
        _abilityDetailsPanel.Init(SelectedShaman,abilitySo,upgradeConfig?.AbilitiesToUpgrade);
        _shamanUpgradePanel.SelectAbility(abilityPanelIndex,abilitySo);
        Refresh();
    }

    #endregion
    public void GainExp()
    {
        SelectedShaman.ShamanExperienceHandler.ManualExpGain();
        Refresh();
    }

    public void ResetSkillPoints()
    {
        SelectedShaman.ShamanExperienceHandler.ResetSkillPoints();
        SelectedShaman.AbilityUpgrades = new();
        SelectedShaman.StatUpgrades = new();
        Refresh();
    }

    public override void Hide()
    {
        _shamanUpgradePanel.OnStatUpgrade -= _shamanDetailsPanel.AddUpgradeToStats;
        base.Hide();
    }
}