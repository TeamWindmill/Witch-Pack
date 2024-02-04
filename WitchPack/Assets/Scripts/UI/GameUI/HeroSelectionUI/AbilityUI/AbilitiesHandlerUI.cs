using System.Collections.Generic;
using UnityEngine;

public class AbilitiesHandlerUI : MonoBehaviour
{
    [SerializeField] private AbilityUIButton[] abilityUIButtons;
    [SerializeField] private AbilityUpgradePanelUI abilityUpgradePanelUI;

    private Shaman _shaman;

    public void Show(Shaman shaman)
    {
        _shaman = shaman;
        abilityUpgradePanelUI.SetShaman(shaman);
        abilityUpgradePanelUI.OnAbilityUpgrade += OnAbilityUpgrade;
        shaman.EnergyHandler.OnShamanLevelUp += OnShamanLevelUp;
        var rootAbilities = shaman.RootAbilities;
        foreach (var uiBlock in abilityUIButtons)
        {
            uiBlock.Hide();
        }

        if (rootAbilities.Count <= 0) return;
        foreach (var rootAbility in rootAbilities)
        {
            var uiButton = GetAvailableButton();
            var activeAbility = shaman.GetActiveAbilityFromRoot(rootAbility);
            var caster = shaman.GetCasterFromAbility(activeAbility);
            uiButton.Init(rootAbility, activeAbility, caster, CheckAbilityUpgradable(activeAbility));
            uiButton.OnAbilityClick += OpenUpgradePanel;
        }

        abilityUpgradePanelUI.Hide();
    }


    public void Hide()
    {
        abilityUpgradePanelUI.OnAbilityUpgrade -= OnAbilityUpgrade;
        _shaman.EnergyHandler.OnShamanLevelUp -= OnShamanLevelUp;
        abilityUpgradePanelUI.Hide();
        foreach (var uiBlock in abilityUIButtons)
        {
            if (!uiBlock.gameObject.activeSelf) return;
            uiBlock.Hide();
        }
    }

    private void OpenUpgradePanel(AbilityUIButton abilityButton)
    {
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
        foreach (var rootAbility in _shaman.RootAbilities)
        {
            var uiButton = GetAvailableButton();
            var activeAbility = _shaman.GetActiveAbilityFromRoot(rootAbility);
            var caster = _shaman.GetCasterFromAbility(activeAbility);
            uiButton.Init(rootAbility, activeAbility, caster, _shaman.EnergyHandler.HasSkillPoints);
            uiButton.OnAbilityClick += OpenUpgradePanel;
        }
    }

    private bool CheckAbilityUpgradable(BaseAbility ability)
    {
        if (!_shaman.EnergyHandler.HasSkillPoints) return false;
        if (ability is not null)
        {
            if (ability.Upgrades.Length == 0) return false;
        }
        return true;
    }
}