using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeWindow : UIWindowManager
{
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;
    [SerializeField] private ShamanDetailsPanel _shamanDetailsPanel;
    [SerializeField] private UpgradesPartyUIPanel _upgradesPartyUIPanel;
    [SerializeField] private List<ShamanSaveData> _shamanRoster;

    private ShamanSaveData _selectedShaman;
    public override void Show()
    {
        _shamanRoster = GameManager.SaveData.ShamanRoster;
        _upgradesPartyUIPanel.Init();
        base.Show();
        SelectShaman(_shamanRoster[0]);
    }

    public void SelectShaman(ShamanSaveData shamanSaveData)
    {
        _selectedShaman = shamanSaveData;
        _shamanUpgradePanel.Init(shamanSaveData);
        _upgradesPartyUIPanel.SelectShamanIcon(shamanSaveData);
        _shamanDetailsPanel.Init(shamanSaveData);
        Refresh();
    }

    public void GainExp()
    {
        _selectedShaman.ShamanExperienceHandler.ManualExpGain();
        Refresh();
    }
}