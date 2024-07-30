using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Shaman;
using Sound;
using UnityEngine;

namespace UI.GameUI.HeroSelectionUI.AbilityUI
{
    public class AbilitiesHandlerUI : MonoBehaviour
    {
        public AbilityUpgradePanelUI AbilityUpgradePanelUI => abilityUpgradePanelUI;

        [SerializeField] private AbilityUIButton[] abilityUIButtons;
        [SerializeField] private AbilityUpgradePanelUI abilityUpgradePanelUI;

        private Shaman _shaman;

        public void Show(Shaman shaman)
        {
            _shaman = shaman;
            abilityUpgradePanelUI.SetShaman(shaman);
            abilityUpgradePanelUI.OnAbilityUpgrade += OnAbilityUpgrade;
            shaman.EnergyHandler.OnShamanLevelUp += OnShamanLevelUp;
            var rootAbilities = shaman.ShamanAbilityHandler.RootAbilities;
            foreach (var uiBlock in abilityUIButtons)
            {
                uiBlock.Hide();
            }

            if (rootAbilities.Count <= 0) return;
            foreach (var rootAbility in rootAbilities)
            {
                var uiButton = GetAvailableButton();
                var activeAbility = shaman.ShamanAbilityHandler.GetActiveAbilityFromRoot(rootAbility);
                var caster = shaman.ShamanAbilityHandler.GetCasterFromAbility(activeAbility);
                uiButton.Init(rootAbility, activeAbility, caster, CheckAbilityUpgradable(shaman,activeAbility));
                uiButton.OnAbilityClick += OpenUpgradePanel;
            }

            abilityUpgradePanelUI.Hide();
        }


        public void Hide()
        {
        
            abilityUpgradePanelUI.OnAbilityUpgrade -= OnAbilityUpgrade;
            if(_shaman != null) _shaman.EnergyHandler.OnShamanLevelUp -= OnShamanLevelUp;
            abilityUpgradePanelUI.Hide();
            foreach (var uiBlock in abilityUIButtons)
            {
                if (!uiBlock.gameObject.activeSelf) return;
                uiBlock.Hide();
            }
        }

        private void OpenUpgradePanel(AbilityUIButton abilityButton)
        {
            SoundManager.PlayAudioClip(SoundEffectType.OpenUpgradeTree);
            abilityUpgradePanelUI.Init(abilityButton);
        }

        private AbilityUIButton GetAvailableButton()
        {
            foreach (var uiButton in abilityUIButtons)
            {
                if (uiButton.gameObject.activeSelf) continue;
                return uiButton;
            }

            return null;
        }

        private void OnShamanLevelUp(int obj)
        {
            if (abilityUpgradePanelUI.gameObject.activeSelf)
            {
                abilityUpgradePanelUI.Show();
            }
            else
            {
                Show(_shaman);
            }
        }

        private void OnAbilityUpgrade()
        {
            foreach (var uiButton in abilityUIButtons)
            {
                uiButton.Hide();
            }
            foreach (var rootAbility in _shaman.ShamanAbilityHandler.RootAbilities)
            {
                var uiButton = GetAvailableButton();
                var activeAbility = _shaman.ShamanAbilityHandler.GetActiveAbilityFromRoot(rootAbility);
                var caster = _shaman.ShamanAbilityHandler.GetCasterFromAbility(activeAbility);
                uiButton.Init(rootAbility, activeAbility, caster, CheckAbilityUpgradable(_shaman,activeAbility));
                uiButton.OnAbilityClick += OpenUpgradePanel;
            }
        }

        private static bool CheckAbilityUpgradable(Shaman shaman, Ability ability)
        {
            if (!shaman.EnergyHandler.HasSkillPoints) return false;
            if (ability is not null)
            {
                if (ability.Upgrades.Count == 0) return false;
            }
            return true;
        }
    }
}