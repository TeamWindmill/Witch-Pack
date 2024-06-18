using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMetaUpgrade : UIElement
{
    [SerializeField] private TextMeshProUGUI _abilityName;
    [SerializeField] private Image _abilityIcon;

    [SerializeField] private AbilityMetaUpgradeIcon[] _abilityUpgradeIcons;
    private ShamanUpgradePanel _shamanUpgradePanel;

    private void Start()
    {
        _abilityUpgradeIcons.ForEach(i => i.OnUpgrade += _shamanUpgradePanel.AddUpgradeToShaman);
    }

    public void Init(ShamanUpgradePanel shamanUpgradePanel,AbilityPanelUpgrades abilityPanelConfig,bool hasSkillPoint)
    {
        _shamanUpgradePanel = shamanUpgradePanel;
        _abilityName.text = abilityPanelConfig.Ability.Name;
        _abilityIcon.sprite = abilityPanelConfig.Ability.DefaultIcon;
        
        for (int i = 0; i < _abilityUpgradeIcons.Length; i++)
        {
            if(abilityPanelConfig.StatUpgrades.Count - 1 < i) continue;
            _abilityUpgradeIcons[i].Init(abilityPanelConfig.StatUpgrades[i],hasSkillPoint);
            if(_abilityUpgradeIcons[i].OpenAtStart && !abilityPanelConfig.StatUpgrades[i].NotWorking) _abilityUpgradeIcons[i].ChangeState(UpgradeState.Open);
        }
    }
}


