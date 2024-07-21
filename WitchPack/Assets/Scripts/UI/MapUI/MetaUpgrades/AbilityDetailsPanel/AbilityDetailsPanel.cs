using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDetailsPanel : UIElement<ShamanSaveData,AbilitySO>
{
    [SerializeField] private TextMeshProUGUI abilityNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TMP_Dropdown TargetingOptionsDropdown;
    [SerializeField] private TMP_Dropdown TargetModifierDropdown;
    [SerializeField] private AbilityStatBlockUI[] abilityStatBlocks;

    private AbilitySO _abilityConfig;
    private ShamanSaveData _shamanSaveData;

    private void Start()
    {
        TargetingOptionsDropdown.options.Clear();
        TargetModifierDropdown.options.Clear();
        Enum.GetNames(typeof(TargetPriority)).ToList().ForEach(x => TargetingOptionsDropdown.options.Add(new TMP_Dropdown.OptionData(x)));
        Enum.GetNames(typeof(TargetModifier)).ToList().ForEach(x => TargetModifierDropdown.options.Add(new TMP_Dropdown.OptionData(x)));
    }

    public override void Init(ShamanSaveData shamanSaveData, AbilitySO abilitySO)
    {
        _shamanSaveData = shamanSaveData;
        _abilityConfig = abilitySO;
        descriptionText.text = abilitySO.Discription;
        abilityNameText.text = abilitySO.Name;

        
        AbilityStatInit(abilitySO);
        base.Init(shamanSaveData,abilitySO);
    }

    

    public void ChangeTargeting(TargetData targetData)
    {
        
    }
    private void AbilityStatInit(AbilitySO abilitySO)
    {
        var ability = AbilityFactory.CreateAbility(abilitySO,null);
        foreach (var abilityUpgradeConfig in _shamanSaveData.AbilityUpgrades)
        {
            if (abilityUpgradeConfig.AbilitiesToUpgrade.Contains(abilitySO))
            {
                ability.AddStatUpgrade(abilityUpgradeConfig);
            }
        }
        foreach (var abilityStatBlock in abilityStatBlocks)
        {
            abilityStatBlock.Init(ability.GetAbilityStat(abilityStatBlock.StatTypeId));
        }
    }
}