using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void Init(ShamanUpgradePanel shamanUpgradePanel,List<StatUpgradeConfig> statUpgradeConfigs,bool hasSkillPoint)
    {
        _shamanUpgradePanel = shamanUpgradePanel;
        _abilityName.text = _title;
        
        for (int i = 0; i < _statUpgradeIcons.Length; i++)
        {
            if(statUpgradeConfigs.Count - 1 < i) continue;
            _statUpgradeIcons[i].Init(statUpgradeConfigs[i],hasSkillPoint);
            if(_statUpgradeIcons[i].OpenAtStart) _statUpgradeIcons[i].ChangeStateVisuals(UpgradeState.Open);
        }
    }
}
