using TMPro;
using UnityEngine;


public class EndScreenUIHandler : UIElement
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _tips;
    [SerializeField] private ItemsGainedPanel _itemsGainedPanel;
    [SerializeField] private EndScreenTextsConfig _config;

    
    public override void Show()
    {
        base.Show();
        var isWon = LevelManager.Instance.IsWon;
        _title.text = isWon ? "You won!" : "You lost!";
        _tips.text = isWon ? _config.WinText[Random.Range(0, _config.WinText.Length)] : _config.LoseText[Random.Range(0, _config.LoseText.Length)];
        if (isWon)
        {
            _itemsGainedPanel.ShowWinPanel();
        }
        else
        {
            _itemsGainedPanel.ShowLosePanel();
        }
    }

    public void ReturnToMap()
    {
        gameObject.SetActive(false);
        BgMusicManager.Instance.PlayMusic(MusicClip.MenuMusic);
        GameManager.SceneHandler.LoadScene(SceneType.Map);
        if (LevelManager.Instance.CurrentLevel.Config.AfterDialog != null)
        {
            DialogBox.Instance.SetDialogSequence(LevelManager.Instance.CurrentLevel.Config.AfterDialog);
            DialogBox.Instance.Show();
        }
    }
}