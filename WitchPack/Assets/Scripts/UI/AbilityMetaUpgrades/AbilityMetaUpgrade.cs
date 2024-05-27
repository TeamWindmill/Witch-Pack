using System;
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

    private AbilityPanelUpgrades _abilityPanelUpgrades;
    private ShamanUpgradePanel _shamanUpgradePanel;

    private void Start()
    {
        _abilityUpgradeIcons.ForEach(i => i.OnUpgrade += _shamanUpgradePanel.AddUpgradeToShaman);
    }

    public void Init(ShamanUpgradePanel shamanUpgradePanel,AbilityPanelUpgrades abilityPanelConfig,bool hasSkillPoint)
    {
        _shamanUpgradePanel = shamanUpgradePanel;
        _abilityPanelUpgrades = abilityPanelConfig;
        _abilityName.text = abilityPanelConfig.Ability.Name;
        _abilityIcon.sprite = abilityPanelConfig.Ability.DefaultIcon;
        var abilities = new List<AbilitySO>();
        abilities.Add(abilityPanelConfig.Ability); 
        abilities.AddRange(abilityPanelConfig.Ability.GetUpgrades());
        
        for (int i = 0; i < _abilityUpgradeIcons.Length; i++)
        {
            if(_abilityPanelUpgrades.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(_abilityPanelUpgrades.StatUpgrades[i],abilities.ToArray(),hasSkillPoint);
            if(_abilityUpgradeIcons[i].OpenAtStart) _abilityUpgradeIcons[i].ChangeStateVisuals(AbilityUpgradeState.Open);
        }
    }
}

public readonly struct AbilityUpgrade
{
    public readonly AbilityStatUpgradeConfig StatUpgradeConfig;
    public readonly AbilitySO[] AbilitiesToUpgrade;

    public AbilityUpgrade(AbilityStatUpgradeConfig statUpgradeConfig, AbilitySO[] abilitiesToUpgrade)
    {
        StatUpgradeConfig = statUpgradeConfig;
        AbilitiesToUpgrade = abilitiesToUpgrade;
    }
}
