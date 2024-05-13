using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class PartySelectionWindow : UIElement
{
    [SerializeField] private EnemyPanelConfig _enemyPanelConfig;
    [SerializeField] private RosterPanel _rosterPanel;
    [SerializeField] private PackPanel _packPanel;
    [SerializeField] private EnemyPanel _enemyPanel;
    [SerializeField] private TextMeshProUGUI _levelTitle;
    public List<ShamanConfig> ActiveShamanParty { get; private set; }
    private LevelConfig _levelConfig;

    public override void Show()
    {
        _levelConfig = GameManager.Instance.CurrentLevelConfig;
        ActiveShamanParty = new();
        _rosterPanel.Init(this,GameManager.Instance.ShamansManager.ShamanRoster);
        _packPanel.Init(this);
        _enemyPanel.Init(_levelConfig,_enemyPanelConfig);
        _levelTitle.text = $"Level {_levelConfig.Number} - {_levelConfig.Name}";
        base.Show();
    }

    public override void Hide()
    {
        _rosterPanel.Hide();
        _enemyPanel.Hide();
        base.Hide();
    }

    public void StartLevel()
    {
        if (ActiveShamanParty.Count == 0)
        {
            _packPanel.FlashInRed();
            return;
        }
        GameManager.Instance.CurrentLevelConfig.Shamans = ActiveShamanParty.ToArray();
        GameManager.SceneHandler.LoadScene(SceneType.Game);
    }

    public void AssignShamanToPack(ShamanConfig shaman)
    {
        ActiveShamanParty.Add(shaman);
        _packPanel.AddShamanToPack(shaman);
        _rosterPanel.AssignShaman(shaman);
    }
    public void UnassignShamanFromPack(ShamanConfig shaman)
    {
        ActiveShamanParty.Remove(shaman);
        _rosterPanel.UnassignShaman(shaman);
        _packPanel.RemoveShamanFromPack(shaman);
    }

    protected override void OnValidate()
    {
        //_enemyPanelConfig.EnemyAmounts.ForEach((amount) => amount.RoundToInt());
        base.OnValidate();
    }
}
