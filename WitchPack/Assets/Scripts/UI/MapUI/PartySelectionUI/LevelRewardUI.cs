using TMPro;
using UI.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MapUI.PartySelectionUI
{
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
}
