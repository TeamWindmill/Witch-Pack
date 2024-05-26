using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMetaUpgrade : UIElement
{
    [SerializeField] private AbilityMetaUpgradeIcon[] _abilityUpgradeIcons;

    [SerializeField] private TextMeshProUGUI _abilityName;
    [SerializeField] private Image _abilityIcon;

    private AbilitySO[] _abilitiesSo;
    private AbilityPanelUpgrades _abilityPanelUpgrades;
    
    public void Init(AbilityPanelUpgrades abilityPanelConfig,bool hasSkillPoint)
    {
        _abilityPanelUpgrades = abilityPanelConfig;
        _abilityName.text = abilityPanelConfig.Ability.Name;
        _abilityIcon.sprite = abilityPanelConfig.Ability.DefaultIcon;
        var abilities = new List<AbilitySO>();
        abilities.Add(abilityPanelConfig.Ability); 
        abilities.AddRange(abilityPanelConfig.Ability.GetUpgrades());
        _abilitiesSo = abilities.ToArray();
        
        for (int i = 0; i < _abilityUpgradeIcons.Length; i++)
        {
            if(_abilityPanelUpgrades.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(_abilityPanelUpgrades.StatUpgrades[i],hasSkillPoint); 
            if(_abilityUpgradeIcons[i].OpenAtStart) _abilityUpgradeIcons[i].ChangeState(AbilityUpgradeState.Open);
        }
    }
}
