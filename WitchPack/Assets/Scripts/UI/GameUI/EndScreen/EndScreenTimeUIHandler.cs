using GameTime;
using UI.UISystem;
using UnityEngine;

namespace UI.GameUI.EndScreen
{
    public class EndScreenTimeUIHandler : UIElement
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text;

        public void StartTimer()
        {
        
        }

        public override void Show()
        {
            base.Show();
            _text.text = $"{(int)(GAME_TIME.TimePlayed / 60)} : {GAME_TIME.TimePlayed % 60:00}";
        }
    }
}