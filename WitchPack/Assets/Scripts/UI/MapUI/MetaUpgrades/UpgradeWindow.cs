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

    public ShamanSaveData SelectedShaman { get; private set; }
    public override void Show()
    {
        _shamanRoster = GameManager.SaveData.ShamanRoster;
        _upgradesPartyUIPanel.Init();
        _shamanUpgradePanel.OnStatUpgrade += _shamanDetailsPanel.AddUpgradeToStats;
        base.Show();
        SelectShaman(_shamanRoster[0]);
        SelectAbility(SelectedShaman.Config.RootAbilities[0]);
    }

    public void SelectShaman(ShamanSaveData shamanSaveData)
    {
        SelectedShaman = shamanSaveData;
        _shamanUpgradePanel.Init(shamanSaveData);
        _upgradesPartyUIPanel.SelectShamanIcon(shamanSaveData);
        _shamanDetailsPanel.Init(shamanSaveData);
        Refresh();
    }

    public void SelectAbility(AbilitySO abilitySo)
    {
        _abilityDetailsPanel.Init(SelectedShaman,abilitySo);
        Refresh();
    }

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