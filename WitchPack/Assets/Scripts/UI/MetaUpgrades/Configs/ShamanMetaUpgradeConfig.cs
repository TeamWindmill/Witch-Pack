using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ShamanMetaUpgradeConfig",fileName = "ShamanMetaUpgradeConfig")]
public class ShamanMetaUpgradeConfig : SerializedScriptableObject
{
    [SerializeField] private ShamanConfig _shamanConfig;
    [SerializeField] private List<AbilityPanelUpgrades> _abilityPanelUpgrades;
    [SerializeField] private StatPanelUpgrades _statPanelUpgrades;

    public List<AbilityPanelUpgrades> AbilityPanelUpgrades => _abilityPanelUpgrades;
    public StatPanelUpgrades StatPanelUpgrades => _statPanelUpgrades;

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
    [BoxGroup("Left")] public List<AbilityUpgradeConfig> LeftStatUpgrades;
    [BoxGroup("Right")] public List<AbilityUpgradeConfig> RightStatUpgrades ;

    public List<AbilityUpgradeConfig> StatUpgrades
    {
        get
        {
            List<AbilityUpgradeConfig> upgrades = new();
            upgrades.AddRange(LeftStatUpgrades);
            upgrades.AddRange(RightStatUpgrades);
            return upgrades;
        }
    }


    public AbilityPanelUpgrades(AbilitySO ability)
    {
        Ability = ability;
        LeftStatUpgrades = new();
        RightStatUpgrades = new();
    }
}

[Serializable]
public struct StatPanelUpgrades
{
    public List<StatUpgradeConfig> LeftStatUpgrades;
    public List<StatUpgradeConfig> RightStatUpgrades;

    public List<StatUpgradeConfig> StatUpgrades
    {
        get
        {
            List<StatUpgradeConfig> upgrades = new();
            upgrades.AddRange(LeftStatUpgrades);
            upgrades.AddRange(RightStatUpgrades);
            return upgrades;
        }
    }
}