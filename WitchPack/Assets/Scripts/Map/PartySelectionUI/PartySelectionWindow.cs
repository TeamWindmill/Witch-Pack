using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartySelectionWindow : UIElement
{
    [SerializeField] private EnemyPanelConfig _enemyPanelConfig;
    [SerializeField] private RosterPanel _rosterPanel;
    [SerializeField] private PackPanel _packPanel;
    [SerializeField] private EnemyPanel _enemyPanel;
    [SerializeField] private RewardsPanel _rewardsPanel;
    [SerializeField] private TextMeshProUGUI _levelTitle;
    public List<ShamanSaveData> ActiveShamanParty { get; private set; }
    private LevelConfig _levelConfig;

    public override void Show()
    {
        _levelConfig = GameManager.Instance.CurrentLevelConfig;
        ActiveShamanParty = new();
        _rosterPanel.Init(this, GameManager.Instance.ShamansManager.ShamanRoster);
        _packPanel.Init(this);
        _enemyPanel.Init(_levelConfig, _enemyPanelConfig);
        _rewardsPanel.Init(_levelConfig);
        _levelTitle.text = $"Level {_levelConfig.Number} - {_levelConfig.Name}";
        base.Show();
    }

    public override void Hide()
    {
        _rosterPanel.Hide();
        _enemyPanel.Hide();
        _rewardsPanel.Hide();
        MapManager.Instance.Init();
        base.Hide();
    }

    public void StartLevel()
    {
        if (ActiveShamanParty.Count == 0)
        {
            _packPanel.FlashInRed();
            return;
        }

        GameManager.Instance.CurrentLevelConfig.SelectedShamans = ActiveShamanParty;
        
        base.Hide();
        
        if (_levelConfig.BeforeDialog != null)
        {
            DialogBox.Instance.SetDialogSequence(_levelConfig.BeforeDialog, () => GameManager.SceneHandler.LoadScene(SceneType.Game));
            DialogBox.Instance.Show();
        }
        else GameManager.SceneHandler.LoadScene(SceneType.Game);
    }

    public void AssignShamanToPack(ShamanSaveData shaman)
    {
        ActiveShamanParty.Add(shaman);
        _packPanel.AddShamanToPack(shaman);
        _rosterPanel.AssignShaman(shaman);
    }

    public void UnassignShamanFromPack(ShamanSaveData shaman)
    {
        ActiveShamanParty.Remove(shaman);
        _rosterPanel.UnassignShaman(shaman);
        _packPanel.RemoveShamanFromPack(shaman);
    }
}