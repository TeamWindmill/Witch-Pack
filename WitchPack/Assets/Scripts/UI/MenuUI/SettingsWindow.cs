using UI.UISystem;
using UnityEngine;

namespace UI.MenuUI
{
    public class SettingsWindow : UIElement
    {
        [SerializeField] private Canvas _settingsCanvas;
        public override void Show()
        {
            _settingsCanvas.gameObject.SetActive(true);
            base.Show();
        }

        public override void Hide()
        {
            _settingsCanvas.gameObject.SetActive(false);
            base.Hide();
        }
    }
}