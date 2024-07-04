using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeWindow : UIWindowManager
{
    [SerializeField] private TextMeshProUGUI _shamanNameText;
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;
    [SerializeField] private List<ShamanSaveData> _shamanRoster;
    public override void Show()
    {
        _shamanRoster = GameManager.SaveData.ShamanRoster;
        SelectShaman(_shamanRoster[0]);
        base.Show();
    }

    private void SelectShaman(ShamanSaveData shamanSaveData)
    {
        _shamanUpgradePanel.Init(shamanSaveData);
        _shamanNameText.text = shamanSaveData.Config.Name;
        Refresh();
    }
}