using Managers;
using TMPro;
using UI.UISystem;
using UnityEngine;

namespace UI.GameUI.EndScreen
{
    public class ItemsGainedPanel : UIElement
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private ShamanIconUI _shamanIconPrefab;
        [SerializeField] private Transform _shamanIconsHolder;
        public void ShowWinPanel()
        {
            _titleText.text = "Shamans added to your party:";
            foreach (var shaman in LevelManager.Instance.CurrentLevel.Config.shamansToAddAfterComplete)
            {
                var shamanIcon = Instantiate(_shamanIconPrefab, _shamanIconsHolder);
                shamanIcon.Init(shaman);
            }
        }

        public void ShowLosePanel()
        {
            _titleText.text = "You Gained Nothing...";
        }
    }
}