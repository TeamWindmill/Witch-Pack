
using UnityEngine;

namespace Tzipory.GameplayLogic.UIElements
{
    public class CoreHPUIHnadler : BaseUIElement
    {

        public override void Init()
        {
            //_maxCount.text = $"/{LevelManager.CoreTemplete.EntityHealthComponent.Health.BaseValue}";
        }

        public override void Show()
        {

            //LevelManager.CoreTemplete.EntityHealthComponent.Health.OnValueChanged += UpdateCoreUI;
        }

        private void UpdateCoreUI()
        {
            UpdateUIVisual();
        }

        public override void UpdateUIVisual()
        {
            //UpdateUiData(LevelManager.CoreTemplete.EntityHealthComponent.Health.CurrentValue);
        }

        public override void Hide()
        {
            //LevelManager.CoreTemplete.EntityHealthComponent.Health.OnValueChanged -= UpdateCoreUI;
        }
    }
}