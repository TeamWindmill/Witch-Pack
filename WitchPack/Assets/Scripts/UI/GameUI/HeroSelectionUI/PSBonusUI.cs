using Gameplay.Units.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI.HeroSelectionUI
{
    public class PSBonusUI : MonoBehaviour
    {
        [SerializeField] private StatType statBonusType;
        [SerializeField] private Sprite _psIconSprite;
        [SerializeField] private TextMeshProUGUI _bonusText;
        [SerializeField] private Image _splash;

        public StatType StatBonusType => statBonusType;

        public void Show(float bonusValue)
        {
            gameObject.SetActive(true);
            _splash.sprite = _psIconSprite;
            string bonusText = $"+{bonusValue}%";
            _bonusText.text = bonusText;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
