using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class StatMetaUpgrade : UIElement
{
    [SerializeField] private string _title;
    [SerializeField] private TextMeshProUGUI _abilityName;
    
    [SerializeField] private StatMetaUpgradeIcon[] _statUpgradeIcons;
    private ShamanUpgradePanel _shamanUpgradePanel;
    
    private void Start()
    {
        _statUpgradeIcons.ForEach(i => i.OnUpgrade += _shamanUpgradePanel.AddUpgradeToShaman);
    }

    public void Init(ShamanUpgradePanel shamanUpgradePanel,List<StatUpgradeConfig> statUpgradeConfigs)
    {
        _shamanUpgradePanel = shamanUpgradePanel;
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
}
