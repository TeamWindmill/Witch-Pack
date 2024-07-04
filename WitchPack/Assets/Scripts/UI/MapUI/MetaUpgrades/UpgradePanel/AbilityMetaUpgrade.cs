using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMetaUpgrade : UIElement
{
    [SerializeField] private TextMeshProUGUI _abilityName;
    [SerializeField] private Image _abilityIcon;

    [SerializeField] private AbilityMetaUpgradeIcon[] _abilityUpgradeIcons;
    [SerializeField] private ShamanUpgradePanel _shamanUpgradePanel;
    private AbilityPanelUpgrades _abilityPanelConfig;

    private void Start()
    {
        _abilityUpgradeIcons.ForEach(i => i.OnUpgrade += _shamanUpgradePanel.AddUpgradeToShaman);
    }

    public void Init(AbilityPanelUpgrades abilityPanelConfig)
    {
        _abilityPanelConfig = abilityPanelConfig;
        _abilityName.text = abilityPanelConfig.Ability.Name;
        _abilityIcon.sprite = abilityPanelConfig.Ability.DefaultIcon;
        var availableSkillPoints = _shamanUpgradePanel.ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints;
        for (int i = 0; i < _abilityUpgradeIcons.Length; i++)
        {
            if(abilityPanelConfig.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(abilityPanelConfig.StatUpgrades[i],availableSkillPoints);
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
            if(_abilityPanelConfig.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(_abilityPanelConfig.StatUpgrades[i],availableSkillPoints);
            if(_abilityUpgradeIcons[i].OpenAtStart && !_abilityPanelConfig.StatUpgrades[i].NotWorking) _abilityUpgradeIcons[i].ChangeState(UpgradeState.Open);
        }
        foreach (var upgradeIcon in _abilityUpgradeIcons)
        {
            foreach (var abilityUpgrade in _shamanUpgradePanel.ShamanSaveData.AbilityUpgrades)
            {
                if(ReferenceEquals(abilityUpgrade,upgradeIcon.UpgradeConfig)) upgradeIcon.ChangeState(UpgradeState.Upgraded);
            }
        }
    }
}


