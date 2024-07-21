using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeWindow : UIWindowManager
{
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;
    [SerializeField] private ShamanDetailsPanel _shamanDetailsPanel;
    [SerializeField] private AbilityDetailsPanel _abilityDetailsPanel;
    [SerializeField] private UpgradesPartyUIPanel _upgradesPartyUIPanel;
    [SerializeField] private List<ShamanSaveData> _shamanRoster;

    private ShamanSaveData _selectedShaman;
    public override void Show()
    {
        _shamanRoster = GameManager.SaveData.ShamanRoster;
        _upgradesPartyUIPanel.Init();
        _shamanUpgradePanel.OnStatUpgrade += _shamanDetailsPanel.AddUpgradeToStats;
        base.Show();
        SelectShaman(_shamanRoster[0]);
        SelectAbility(_selectedShaman.Config.RootAbilities[0]);
    }

    public void SelectShaman(ShamanSaveData shamanSaveData)
    {
        _selectedShaman = shamanSaveData;
        _shamanUpgradePanel.Init(shamanSaveData);
        _upgradesPartyUIPanel.SelectShamanIcon(shamanSaveData);
        _shamanDetailsPanel.Init(shamanSaveData);
        Refresh();
    }

    public void SelectAbility(AbilitySO abilitySo)
    {
        _abilityDetailsPanel.Init(_selectedShaman,abilitySo);
        Refresh();
    }

    public void GainExp()
    {
        _selectedShaman.ShamanExperienceHandler.ManualExpGain();
        Refresh();
    }

    public void ResetSkillPoints()
    {
        _selectedShaman.ShamanExperienceHandler.ResetSkillPoints();
        _selectedShaman.AbilityUpgrades = new();
        _selectedShaman.StatUpgrades = new();
        Refresh();
    }

    public override void Hide()
    {
        _shamanUpgradePanel.OnStatUpgrade -= _shamanDetailsPanel.AddUpgradeToStats;
        base.Hide();
    }
}