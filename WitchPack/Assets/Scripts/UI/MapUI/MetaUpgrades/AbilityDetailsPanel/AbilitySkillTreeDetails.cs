using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class AbilitySkillTreeDetails : UIElement<AbilityDetailsPanel,AbilitySO>
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private AbilitySkillTreeIconDetails baseAbilityUpgradeUIButton;
    [SerializeField] private Transform upgrades3Holder;
    [SerializeField] private Transform upgrades3BG;
    [SerializeField] private AbilitySkillTreeIconDetails[] abilityUpgrades3UI;
    [SerializeField] private Transform upgrades2Holder;
    [SerializeField] private Transform upgrades2BG;
    [SerializeField] private AbilitySkillTreeIconDetails[] abilityUpgrades2UI;

    private AbilityDetailsPanel _detailsPanel;
    private AbilitySO _rootAbility;
    private List<AbilitySO> _abilityUpgrades;

    public override void Init(AbilityDetailsPanel detailsPanel,AbilitySO selectedAbility)
    {
        Hide();
        _detailsPanel = detailsPanel;
        _rootAbility = selectedAbility.RootAbility;
        title.text = selectedAbility.Name;
        IconsInit(_rootAbility);
        SelectIcon(selectedAbility);
        base.Init(detailsPanel,selectedAbility);
        Show();
    }
    
    public override void Hide()
    {
        upgrades3BG.gameObject.SetActive(false);
        upgrades3Holder.gameObject.SetActive(false);
        upgrades2BG.gameObject.SetActive(false);
        upgrades2Holder.gameObject.SetActive(false);
        base.Hide();
    }

    public void HighlightIcons(AbilitySO[] abilitiesToHighlight)
    {
        if (abilitiesToHighlight == null) return;
        //enable selected
        foreach (var ability in abilitiesToHighlight)
        {
            if(ability == _rootAbility) baseAbilityUpgradeUIButton.HighlightIcon(true); //highlight root ability
            
            for (var i = 0; i < _abilityUpgrades.Count; i++) //highlight upgrades
            {
                if (_abilityUpgrades[i] == ability)
                {
                    switch (_abilityUpgrades.Count)
                    {
                        case 3:
                            abilityUpgrades3UI[i].HighlightIcon(true);
                            break;
                        case 2:
                            abilityUpgrades2UI[i].HighlightIcon(true);
                            break;
                    }
                }
            }
        }
    }

    public void DisableHighlightOnAllIcons()
    {
        baseAbilityUpgradeUIButton.HighlightIcon(false);
        if (_abilityUpgrades.Count == 3) abilityUpgrades3UI.ForEach(ability => ability.HighlightIcon(false));
        else if (_abilityUpgrades.Count == 2) abilityUpgrades2UI.ForEach(ability => ability.HighlightIcon(false));
    }
    private void IconsInit(AbilitySO rootAbility)
    {
        _abilityUpgrades = rootAbility.GetUpgrades();
        baseAbilityUpgradeUIButton.Init(rootAbility,SelectIcon);
        if (_abilityUpgrades.Count == 3)
        {
            upgrades3BG.gameObject.SetActive(true);
            upgrades3Holder.gameObject.SetActive(true);
            for (int i = 0; i < _abilityUpgrades.Count; i++)
            {
                abilityUpgrades3UI[i].Init(_abilityUpgrades[i],SelectIcon);
            }
        }
        else if (_abilityUpgrades.Count == 2)
        {
            upgrades2BG.gameObject.SetActive(true);
            upgrades2Holder.gameObject.SetActive(true);
            for (int i = 0; i < _abilityUpgrades.Count; i++)
            {
                abilityUpgrades2UI[i].Init(_abilityUpgrades[i],SelectIcon);
            }
        }
    }

    private void SelectIcon(AbilitySO ability)
    {
        //disable all
        baseAbilityUpgradeUIButton.SelectIcon(false);
        if (_abilityUpgrades.Count == 3) abilityUpgrades3UI.ForEach(ability => ability.SelectIcon(false));
        else if (_abilityUpgrades.Count == 2) abilityUpgrades2UI.ForEach(ability => ability.SelectIcon(false));
        
        //enable selected
        if (ability == _rootAbility) baseAbilityUpgradeUIButton.SelectIcon(true);
        else
        {
            for (int i = 0; i < _abilityUpgrades.Count; i++)
            {
                if (_abilityUpgrades[i] == ability)
                {
                    switch (_abilityUpgrades.Count)
                    {
                        case 3:
                            abilityUpgrades3UI[i].SelectIcon(true);
                            break;
                        case 2:
                            abilityUpgrades2UI[i].SelectIcon(true);
                            break;
                    }
                }
            }
        }
        
        title.text = ability.Name;
        _detailsPanel.DisplayAbility(ability);
    }
}