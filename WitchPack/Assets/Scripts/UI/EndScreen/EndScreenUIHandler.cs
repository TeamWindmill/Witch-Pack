using TMPro;
using UnityEngine;


public class EndScreenUIHandler : UIElement
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _tips;
    [SerializeField] private ShamanIconUI _shamanIconPrefab;
    [SerializeField] private Transform _shamanIconsHolder;
    [SerializeField] private EndScreenTextsConfig _config;

    
    public override void Show()
    {
        base.Show();
        var isWon = LevelManager.Instance.IsWon;
        foreach (var shaman in LevelManager.Instance.CurrentLevel.Config.shamansToAddAfterComplete)
        {
            var shamanIcon = Instantiate(_shamanIconPrefab, _shamanIconsHolder);
            shamanIcon.Init(shaman);
        }
        _title.text = isWon ? "You won!" : "You lost!";
        _tips.text = isWon ? _config.WinText[Random.Range(0, _config.WinText.Length)] : _config.LoseText[Random.Range(0, _config.LoseText.Length)];
    }

    public void ReturnToMap()
    {
        gameObject.SetActive(false);
        BgMusicManager.Instance.PlayMusic(MusicClip.MenuMusic);
        GameManager.SceneHandler.LoadScene(SceneType.Map);
    }
}