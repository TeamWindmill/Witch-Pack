using System;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityMetaUpgrade : UIElement
{
    public int Index { get; private set; }
    public AbilityPanelUpgrades AbilityPanelConfig { get;private set; }
    [SerializeField] private TextMeshProUGUI _abilityName;
    [SerializeField] private Image _abilityFrame;
    [SerializeField]private Sprite _defaultFrame;
    [SerializeField]private Sprite _selectedFrame;
    
    [SerializeField] private AbilityMetaUpgradeIcon[] _abilityUpgradeIcons;
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;

    private void Start()
    {
        _abilityUpgradeIcons.ForEach(icon => icon.OnSelect += _shamanUpgradePanel.SelectAbility);
    }

    public void Init(int index, AbilityPanelUpgrades abilityPanelConfig)
    {
        Index = index;
        AbilityPanelConfig = abilityPanelConfig;
        _abilityName.text = abilityPanelConfig.Ability.Name;
        var availableSkillPoints = _shamanUpgradePanel.ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints;
        for (int i = 0; i < _abilityUpgradeIcons.Length; i++)
        {
            if(abilityPanelConfig.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(Index,abilityPanelConfig.StatUpgrades[i],availableSkillPoints);
            if(_abilityUpgradeIcons[i].OpenAtStart && !abilityPanelConfig.StatUpgrades[i].NotWorking) _abilityUpgradeIcons[i].ChangeState(UpgradeState.Open);
        }
        foreach (var upgradeIcon in _abilityUpgradeIcons)
        {
            foreach (var abilityUpgrade in _shamanUpgradePanel.ShamanSaveData.AbilityUpgrades)
            {
                if(ReferenceEquals(abilityUpgrade,upgradeIcon.UpgradeConfig)) upgradeIcon.ChangeState(UpgradeState.Upgraded);
            }
        }
        
    }

    public override void Refresh()
    {
        var availableSkillPoints = _shamanUpgradePanel.ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints;
        for (int i = 0; i < _abilityUpgradeIcons.Length; i++)
        {
            if(AbilityPanelConfig.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(Index,AbilityPanelConfig.StatUpgrades[i],availableSkillPoints);
            if(_abilityUpgradeIcons[i].OpenAtStart && !AbilityPanelConfig.StatUpgrades[i].NotWorking) _abilityUpgradeIcons[i].ChangeState(UpgradeState.Open);
        }
        foreach (var upgradeIcon in _abilityUpgradeIcons)
        {
            foreach (var abilityUpgrade in _shamanUpgradePanel.ShamanSaveData.AbilityUpgrades)
            {
                if(ReferenceEquals(abilityUpgrade,upgradeIcon.UpgradeConfig)) upgradeIcon.ChangeState(UpgradeState.Upgraded);
            }
        }
    }


    public void SelectAbility(bool state)
    { 
       _abilityFrame.sprite = state ? _selectedFrame : _defaultFrame;
    }

}


