using Managers;
using TMPro;
using UI.UISystem;
using UnityEngine;

namespace UI.GameUI.EndScreen
{
    public class EndScreenWaveInteractiveUIHandler : UIElement
    {
        [SerializeField] private TMP_Text _text;

        public override void Show()
        {
            base.Show();
            _text.text = $"{LevelManager.Instance.CurrentLevel.WaveHandler.CurrentWave}/{LevelManager.Instance.CurrentLevel.WaveHandler.TotalWaves}";
        }
    }
}