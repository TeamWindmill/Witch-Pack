using TMPro;
using UnityEngine;

public class LevelSelectionWindow : UIElement
{
    public EnemyPanel EnemyPanel => _enemyPanel;
    public RewardsPanel RewardsPanel => _rewardsPanel;
    public ChallengesPanel ChallengesPanel => _challengesPanel;
    [SerializeField] private EnemyPanelConfig _enemyPanelConfig;
    [SerializeField] private EnemyPanel _enemyPanel;
    [SerializeField] private RewardsPanel _rewardsPanel;
    [SerializeField] private ChallengesPanel _challengesPanel;
    [SerializeField] private TextMeshProUGUI _levelTitle;
    [SerializeField] private PartySelectionWindow _partySelectionWindow;
    private LevelConfig _levelConfig;


    public override void Show()
    {
        _levelConfig = GameManager.CurrentLevelConfig;

        _enemyPanel.Init(_levelConfig, _enemyPanelConfig);
        _rewardsPanel.Init(_levelConfig);
        _challengesPanel.Init(_levelConfig, _partySelectionWindow);
        _levelTitle.text = $"Level {_levelConfig.Number} - {_levelConfig.Name}";
        base.Show();
    }

    public override void Hide()
    {
        _enemyPanel.Hide();
        _rewardsPanel.Hide();
        MapManager.Instance.Init();
        UIManager.RefreshUIGroup(UIGroup.PartySelectionWindow);
        base.Hide();
    }

    public void StartLevel()
    {
        if (_partySelectionWindow.ActiveShamanParty.Count == 0)
        {
            _partySelectionWindow.FlashInRed();
            return;
        }

        _partySelectionWindow.RefreshActiveParty();
        GameManager.CurrentLevelConfig.SelectedShamans = _partySelectionWindow.ActiveShamanParty;

        base.Hide();

        if (_levelConfig.BeforeDialog != null)
        {
            DialogBox.Instance.SetDialogSequence(_levelConfig.BeforeDialog, () => GameManager.SceneHandler.LoadScene(SceneType.Game));
            DialogBox.Instance.Show();
        }
        else GameManager.SceneHandler.LoadScene(SceneType.Game);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            if (!UIManager.MouseOverUI)
            {
                Hide();
            }
        }
    }
}