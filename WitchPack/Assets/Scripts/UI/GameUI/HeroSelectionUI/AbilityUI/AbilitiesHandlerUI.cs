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
            uiButton.Init(rootAbility,activeAbility, caster);
            uiButton.OnAbilityClick += OpenUpgradePanel;
        }

        abilityUpgradePanelUI.Hide();
    }

    public void Hide()
    {
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
    
}