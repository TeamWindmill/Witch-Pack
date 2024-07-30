using Gameplay.CoreTemple;
using Managers;
using UI.UISystem;

namespace UI.GameUI
{
    public class CoreHPUIHandler : StatBarUIElement
    {
        private CoreTemple _coreTemple;
        public override void Init()
        {
            _coreTemple = LevelManager.Instance.CurrentLevel.CoreTemple;
            ElementInit(_coreTemple.Damageable.MaxHp);
            _coreTemple.Damageable.OnHealthChange += UpdateUI;
        }

        private void UpdateUI(int hp,int maxHp)
        {
            UpdateUIData(_coreTemple.Damageable.CurrentHp);
        }

        public override void Hide()
        {
            _coreTemple.Damageable.OnHealthChange -= UpdateUI;
        }
    }
}