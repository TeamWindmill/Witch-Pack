using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class StatMetaUpgrade : UIElement
{
    [SerializeField] private string _title;
    [SerializeField] private TextMeshProUGUI _abilityName;
    
    [SerializeField] private StatMetaUpgradeIcon[] _statUpgradeIcons;
    [SerializeField]private ShamanUpgradePanel _shamanUpgradePanel;
    private List<StatMetaUpgradeConfig> _statUpgradeConfigs;
    
    private void Start()
    {
        _statUpgradeIcons.ForEach(i => i.OnUpgrade += _shamanUpgradePanel.AddUpgradeToShaman);
    }

    public void Init(List<StatMetaUpgradeConfig> statUpgradeConfigs)
    {
        _statUpgradeConfigs = statUpgradeConfigs;
        _abilityName.text = _title;
        
        for (int i = 0; i < _statUpgradeIcons.Length; i++)
        {
            if(statUpgradeConfigs.Count - 1 < i) continue;
            var availableSkillPoints = _shamanUpgradePanel.ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints;
            _statUpgradeIcons[i].Init(statUpgradeConfigs[i],availableSkillPoints);
            if(_statUpgradeIcons[i].OpenAtStart && !statUpgradeConfigs[i].NotWorking) _statUpgradeIcons[i].ChangeState(UpgradeState.Open);
            
        }

        foreach (var upgradeIcon in _statUpgradeIcons)
        {
            foreach (var abilityUpgrade in _shamanUpgradePanel.ShamanSaveData.StatUpgrades)
            {
                if(ReferenceEquals(abilityUpgrade,upgradeIcon.UpgradeConfig)) upgradeIcon.ChangeState(UpgradeState.Upgraded);
            }
        }
    }
    public override void Refresh()
    {
        for (int i = 0; i < _statUpgradeIcons.Length; i++)
        {
            if(_statUpgradeConfigs.Count - 1 < i) continue;
            var availableSkillPoints = _shamanUpgradePanel.ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints;
            _statUpgradeIcons[i].Init(_statUpgradeConfigs[i],availableSkillPoints);
            if(_statUpgradeIcons[i].OpenAtStart && !_statUpgradeConfigs[i].NotWorking) _statUpgradeIcons[i].ChangeState(UpgradeState.Open);
            
        }

        foreach (var upgradeIcon in _statUpgradeIcons)
        {
            foreach (var abilityUpgrade in _shamanUpgradePanel.ShamanSaveData.StatUpgrades)
            {
                if(ReferenceEquals(abilityUpgrade,upgradeIcon.UpgradeConfig)) upgradeIcon.ChangeState(UpgradeState.Upgraded);
            }
        }
    }
}
