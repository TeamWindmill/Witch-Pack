using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelRewardUI : UIElement
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    public void Init(Sprite icon, string text)
    {
        _icon.sprite = icon;
        _text.text = text;
    }
}
