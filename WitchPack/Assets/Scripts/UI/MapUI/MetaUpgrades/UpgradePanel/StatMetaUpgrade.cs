using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatMetaUpgrade : UIElement
{
    [SerializeField] private string _title;
    [SerializeField] private TextMeshProUGUI _abilityName;
    [SerializeField] private Image _abilityFrame;
    [SerializeField]private ShamanUpgradePanel _shamanUpgradePanel;
    [SerializeField]private Sprite _defaultFrame;
    [SerializeField]private Sprite _selectedFrame;

    [SerializeField] private StatMetaUpgradeIcon[] _statUpgradeIcons;
    private List<StatMetaUpgradeConfig> _statUpgradeConfigs;
    public const int INDEX = 2;

    private void Start()
    {
        _statUpgradeIcons.ForEach(icon => icon.OnSelect += _shamanUpgradePanel.SelectAbility);
    }
    public void Init(List<StatMetaUpgradeConfig> statUpgradeConfigs)
    {
        _statUpgradeConfigs = statUpgradeConfigs;
        _abilityName.text = _title;
        
        for (int i = 0; i < _statUpgradeIcons.Length; i++)
        {
            if(statUpgradeConfigs.Count - 1 < i) continue;
            var availableSkillPoints = _shamanUpgradePanel.ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints;
            _statUpgradeIcons[i].Init(INDEX,statUpgradeConfigs[i],availableSkillPoints);
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
            _statUpgradeIcons[i].Init(INDEX,_statUpgradeConfigs[i],availableSkillPoints);
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

    public void SelectAbility(bool state)
    {
        _abilityFrame.sprite = state ? _selectedFrame : _defaultFrame;
    }
}
