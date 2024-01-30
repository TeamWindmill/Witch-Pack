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
        var baseAbilities = shaman.ActiveAbilities; 
        foreach (var uiBlock in abilityUIButtons)
        {
            uiBlock.Hide();
        }
        if (baseAbilities.Count <= 0) return;
        foreach (var ability in baseAbilities)
        {
            foreach (var uiButton in abilityUIButtons)
            {
                
                if (uiButton.gameObject.activeSelf) continue;
                uiButton.Init(ability,shaman.GetCasterFromAbility(ability));
                uiButton.OnAbilityClick += OpenUpgradePanel;
                break;
            }
        }
        abilityUpgradePanelUI.Hide();
    }
    public void Hide()
    {
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
}
