using System.Linq;
using Gameplay.Targeter;
using Gameplay.Units.Abilities.AbilitySystem;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units;
using Gameplay.Units.Stats;
using Sirenix.Utilities;
using TMPro;
using UI.UISystem;
using UnityEngine;

namespace UI.MapUI.MetaUpgrades.AbilityDetailsPanel
{
    public class AbilityDetailsPanel : UIElement<ShamanSaveData,AbilitySO,AbilitySO[]>
    {
        [SerializeField] private TextMeshProUGUI abilityNameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TMP_Dropdown TargetingOptionsDropdown;
        [SerializeField] private TMP_Dropdown TargetModifierDropdown;
        [SerializeField] private AbilityStatBlockUI[] abilityStatBlocks;
        [SerializeField] private AbilitySkillTreeDetails abilitySkillTree;
        [SerializeField] private Color reductionColor;
        [SerializeField] private Color additionColor;
    
        private AbilitySO _abilityConfig;
        private ShamanSaveData _shamanSaveData;
        private Ability _ability;

        public override void Init(ShamanSaveData shamanSaveData, AbilitySO abilitySO,AbilitySO[] highlightedAbilities = null)
        {
            _shamanSaveData = shamanSaveData;
            _abilityConfig = abilitySO;
            DisplayAbility(abilitySO);
            abilitySkillTree.Init(this,abilitySO);
            abilitySkillTree.DisableHighlightOnAllIcons();
            if(highlightedAbilities != null) abilitySkillTree.HighlightIcons(highlightedAbilities);
            base.Init(shamanSaveData,abilitySO,highlightedAbilities);
        }

        public void DisplayAbility(AbilitySO abilitySO)
        {
            descriptionText.text = abilitySO.Discription;
            abilityNameText.text = abilitySO.Name;
            AbilityStatInit(abilitySO);
        }
        public void ShowStatBonus(AbilityStatType statType,Factor factor, float value)
        {
            abilityStatBlocks.ForEach(block => block.HideBonusStatUI());
            float bonusValue = 0;
            float baseValue = _ability.GetAbilityStatValue(statType);
            foreach (var statBlock in abilityStatBlocks)
            {
                if (statBlock.StatTypeId == statType)
                {
                    switch (factor)
                    {
                        case Factor.Add:
                            bonusValue = value;
                            break;
                        case Factor.Subtract:
                            bonusValue = -value;
                            break;
                        case Factor.Multiply:
                            bonusValue = baseValue * value - baseValue;
                            break;
                    }
                    statBlock.UpdateBonusStatUI(bonusValue);
                    return;
                }
            }
        }

        public void ChangeTargeting(TargetData targetData)
        {
            //needs game design to determine the target data presets
        }
        private void AbilityStatInit(AbilitySO abilitySO)
        {
            _ability = AbilityFactory.CreateAbility(abilitySO,null);
            foreach (var abilityUpgradeConfig in _shamanSaveData.AbilityUpgrades)
            {
                if (abilityUpgradeConfig.AbilitiesToUpgrade.Contains(abilitySO))
                {
                    _ability.AddStatUpgrade(abilityUpgradeConfig);
                }
            }

            //disable all
            foreach (var abilityStatBlock in abilityStatBlocks)
            {
                abilityStatBlock.Hide();
            }
        
            //enable all
            for (int i = 0; i < abilityStatBlocks.Length; i++)
            {
                if(i >= abilitySO.StatTypesForUIDisplay.Length) break;
            
                abilityStatBlocks[i].SetStatType(abilitySO.StatTypesForUIDisplay[i]);
                var stat = _ability.GetAbilityStat(abilityStatBlocks[i].StatTypeId);
                if (stat != null) abilityStatBlocks[i].Init(stat,additionColor,reductionColor);
                else
                {
                    Debug.LogError($"Stat Type - {abilityStatBlocks[i].StatTypeId} was not found in ability - {abilitySO.Name}");
                    abilityStatBlocks[i].Hide();
                }
            }
        }
    }
}