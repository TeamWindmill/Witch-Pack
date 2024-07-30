using Configs;
using TMPro;
using UI.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI.EndScreen
{
    public class ShamanIconUI : UIElement
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _splash;

        public void Init(ShamanConfig shaman)
        {
            _name.text = shaman.Name;
            _splash.sprite = shaman.UnitIcon;
        }
    }
}
