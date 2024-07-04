using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartySelectionWindow : UIElement
{
    #region PanelPointers
    public RosterPanel RosterPanel => _rosterPanel;
    public PackPanel PackPanel => _packPanel;
    public EnemyPanel EnemyPanel => _enemyPanel;
    public RewardsPanel RewardsPanel => _rewardsPanel;
    public ChallengesPanel ChallengesPanel => _challengesPanel;

    #endregion

    [SerializeField] private EnemyPanelConfig _enemyPanelConfig;
    [SerializeField] private RosterPanel _rosterPanel;
    [SerializeField] private PackPanel _packPanel;
    [SerializeField] private EnemyPanel _enemyPanel;
    [SerializeField] private RewardsPanel _rewardsPanel;
    [SerializeField] private ChallengesPanel _challengesPanel;
    [SerializeField] private TextMeshProUGUI _levelTitle;
    public List<ShamanSaveData> ActiveShamanParty { get; private set; }
    public int MaxShamanPartyCap { get; private set; } = DEFAULT_PARTY_SIZE;
    private LevelConfig _levelConfig;

    private const int DEFAULT_PARTY_SIZE = 4;
    public override void Show()
    {
        _levelConfig = GameManager.CurrentLevelConfig;
        ActiveShamanParty = new();
        _rosterPanel.Init(this, GameManager.ShamansManager.ShamanRoster);
        _packPanel.Init(this);
        _enemyPanel.Init(_levelConfig, _enemyPanelConfig);
        _rewardsPanel.Init(_levelConfig);
        _challengesPanel.Init(_levelConfig,this);
        _levelTitle.text = $"Level {_levelConfig.Number} - {_levelConfig.Name}";
        AutoAssignShamansFromRoster();
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

        RefreshActiveParty();
        GameManager.CurrentLevelConfig.SelectedShamans = ActiveShamanParty;
        
        base.Hide();
        
        if (_levelConfig.BeforeDialog != null)
        {
            DialogBox.Instance.SetDialogSequence(_levelConfig.BeforeDialog, () => GameManager.SceneHandler.LoadScene(SceneType.Game));
            DialogBox.Instance.Show();
        }
        else GameManager.SceneHandler.LoadScene(SceneType.Game);
    }

    public void AssignShamanToParty(ShamanSaveData shaman)
    {
        if(ActiveShamanParty.Count >= MaxShamanPartyCap) return;
        ActiveShamanParty.Add(shaman);
        _packPanel.AddShamanToPack(shaman);
        _rosterPanel.RemoveShamanFromRoster(shaman);
    }

    public void UnassignShamanFromParty(ShamanSaveData shaman)
    {
        ActiveShamanParty.Remove(shaman);
        _rosterPanel.AddShamanBackToRoster(shaman);
        _packPanel.RemoveShamanFromPack(shaman);
    }

    public void ReduceShamanSlots(int amountToReduce)
    {
        MaxShamanPartyCap = DEFAULT_PARTY_SIZE;
        MaxShamanPartyCap -= amountToReduce;
        _packPanel.ReduceShamanSlots(MaxShamanPartyCap);
    }

    private void AutoAssignShamansFromRoster()
    {
        var assignedShamans = 0;
        foreach (var packIcon in _packPanel.PackIcons)
        {
            if (packIcon.Assigned) assignedShamans++;
        }
        if(assignedShamans > 0) return;
        foreach (var icon in _rosterPanel.RosterIcons)
        {
            if (icon.ShamanSaveData != null)
            {
                AssignShamanToParty(icon.ShamanSaveData);
            }
        }
    }

    private void RefreshActiveParty()
    {
        ActiveShamanParty = new List<ShamanSaveData>();

        foreach (var icon in _packPanel.PackIcons)
        {
            if (icon.Assigned)
            {
                ActiveShamanParty.Add(icon.ShamanSaveData);
            }
        }
    }
}