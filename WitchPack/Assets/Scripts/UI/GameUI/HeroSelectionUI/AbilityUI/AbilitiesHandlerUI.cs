using System.Collections.Generic;
using UnityEngine;

public class AbilitiesHandlerUI : MonoBehaviour
{
    [SerializeField] private AbilityUIButton[] abilityUIBlocks;
    [SerializeField] private AbilityUpgradePanelUI abilityUpgradePanelUI;

    private Shaman _shaman;
    
    public void Show(Shaman shaman)
    {
        _shaman = shaman;
        var castingHandlers = shaman.CastingHandlers; 
        foreach (var uiBlock in abilityUIBlocks)
        {
            uiBlock.Hide();
        }
        if (castingHandlers.Count <= 0) return;
        foreach (var ability in castingHandlers)
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
    private void OpenUpgradePanel(AbilityUIButton abilityUIButton)
    {
        abilityUpgradePanelUI.Init(abilityUIButton,_shaman);
    }
}
