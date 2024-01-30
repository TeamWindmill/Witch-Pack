using System.Collections.Generic;
using UnityEngine;

public class AbilitiesHandlerUI : MonoBehaviour
{
    [SerializeField] private AbilityUI[] abilityUIBlocks;
    [SerializeField] private AbilityUpgradePanelUI abilityUpgradePanelUI;

    private Shaman _shaman;
    
    public void Show(Shaman shaman)
    {
        _shaman = shaman;
        var abilities = shaman.CastingHandlers; 
        foreach (var uiBlock in abilityUIBlocks)
        {
            uiBlock.Hide();
        }
        if (abilities.Count <= 0) return;
        foreach (var ability in abilities)
        {
            foreach (var uiBlock in abilityUIBlocks)
            {
                
                if (uiBlock.gameObject.activeSelf) continue;
                uiBlock.Init(ability);
                uiBlock.OnAbilityClick += OpenUpgradePanel;
                break;
            }
        }
        abilityUpgradePanelUI.Hide();
    }
    public void Hide()
    {
        foreach (var uiBlock in abilityUIBlocks)
        {
            if (!uiBlock.gameObject.activeSelf) return;
            uiBlock.Hide();
        }
    }
    private void OpenUpgradePanel(AbilityUI abilityUI)
    {
        abilityUpgradePanelUI.Init(abilityUI,_shaman);
    }
}
