using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeWindow : UIWindowManager
{
    [SerializeField] private TextMeshProUGUI _shamanNameText;
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;
    [SerializeField] private UpgradesPartyUIPanel _upgradesPartyUIPanel;
    [SerializeField] private List<ShamanSaveData> _shamanRoster;

    private ShamanSaveData _selectedShaman;
    public override void Show()
    {
        _shamanRoster = GameManager.SaveData.ShamanRoster;
        _upgradesPartyUIPanel.Init();
        SelectShaman(_shamanRoster[0]);
        base.Show();
    }

    public void SelectShaman(ShamanSaveData shamanSaveData)
    {
        _selectedShaman = shamanSaveData;
        _shamanUpgradePanel.Init(shamanSaveData);
        _shamanNameText.text = shamanSaveData.Config.Name;
        Refresh();
    }

    public void GainExp()
    {
        _selectedShaman.ShamanExperienceHandler.ManualExpGain();
        Refresh();
    }
}