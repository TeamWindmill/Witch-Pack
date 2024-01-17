using TMPro;
using UnityEngine;


public class EndScreenUIHandler : UIElement
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _tips;
    [SerializeField] private EndScreenTextsConfig _config;

    
    public override void Show()
    {
        base.Show();
        var isWon = LevelManager.Instance.IsWon;
        _title.text = isWon ? "You won!" : "You lost!";
        _tips.text = isWon ? _config.WinText[Random.Range(0, _config.WinText.Length)] : _config.LoseText[Random.Range(0, _config.LoseText.Length)];
    }
}