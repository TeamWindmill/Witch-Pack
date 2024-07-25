using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDetailsPanel : UIElement<ShamanSaveData,AbilitySO,AbilitySO[]>
{
    [SerializeField] private TextMeshProUGUI abilityNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TMP_Dropdown TargetingOptionsDropdown;
    [SerializeField] private TMP_Dropdown TargetModifierDropdown;
    [SerializeField] private AbilityStatBlockUI[] abilityStatBlocks;
    [SerializeField] private AbilitySkillTreeDetails abilitySkillTree;

    private AbilitySO _abilityConfig;
    private ShamanSaveData _shamanSaveData;

    public override void Init(ShamanSaveData shamanSaveData, AbilitySO abilitySO,AbilitySO[] affectedAbilities = null)
    {
        _shamanSaveData = shamanSaveData;
        _abilityConfig = abilitySO;
        descriptionText.text = abilitySO.Discription;
        abilityNameText.text = abilitySO.Name;
        abilitySkillTree.Init(abilitySO);
        abilitySkillTree.DisableHighlightOnAllIcons();
        if(affectedAbilities != null) abilitySkillTree.HighlightIcons(affectedAbilities);
        AbilityStatInit(abilitySO);
        base.Init(shamanSaveData,abilitySO,affectedAbilities);
    }

    public void ChangeTargeting(TargetData targetData)
    {
        //needs game design to determine the target data presets
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

        for (int i = 0; i < abilityStatBlocks.Length; i++)
        {
            if(i >= abilitySO.StatTypesForUIDisplay.Length) break;
            
            abilityStatBlocks[i].SetStatType(abilitySO.StatTypesForUIDisplay[i]);
            var stat = ability.GetAbilityStat(abilityStatBlocks[i].StatTypeId);
            if (stat != null) abilityStatBlocks[i].Init(stat);
            else
            {
                Debug.LogError($"Stat Type - {abilityStatBlocks[i].StatTypeId} was not found in ability - {abilitySO.Name}");
                abilityStatBlocks[i].Hide();
            }
        }
    }
}