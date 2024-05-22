using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ShamanMetaUpgradeConfig",fileName = "ShamanMetaUpgradeConfig")]
public class ShamanMetaUpgradeConfig : SerializedScriptableObject
{
    [SerializeField] private ShamanConfig _shamanConfig;
    //[TableMatrix(HorizontalTitle = "AbilityUpgrades")]
    [SerializeField] private List<AbilityPanelUpgrades> _abilityPanelUpgrades;

    public List<AbilityPanelUpgrades> AbilityPanelUpgrades => _abilityPanelUpgrades;

    private void OnValidate()
    {
        if (_shamanConfig != null)
        {
            if(_abilityPanelUpgrades.Count > 0) return;
            foreach (var rootAbility in _shamanConfig.RootAbilities)
            {
                _abilityPanelUpgrades.Add(new AbilityPanelUpgrades(rootAbility));
            }
        }
    }


}

[Serializable]
public struct AbilityPanelUpgrades
{
    public AbilitySO Ability;
    public List<AbilityStatUpgrade> StatUpgrades ;

    public AbilityPanelUpgrades(AbilitySO ability)
    {
        Ability = ability;
        StatUpgrades = new();
    }
}
