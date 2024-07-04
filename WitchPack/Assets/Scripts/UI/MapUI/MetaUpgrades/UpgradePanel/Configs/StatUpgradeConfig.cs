using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class StatUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private bool _upgradeAbility;
    [SerializeField] private bool _upgradePassiveAbility;
    [SerializeField,HideIf(nameof(_upgradeAbility))] private StatValueUpgradeConfig[] _stats;
    
    [SerializeField,ShowIf(nameof(_showAbility))] private AbilityStatUpgradeConfig[] _abilityStats;
    [SerializeField,ShowIf(nameof(_showAbility))] private AbilityBehavior[] _abilitiesBehaviors;
    [SerializeField,ShowIf(nameof(_showAbility))] private DamageBoostData[] _damageBoosts;
    [SerializeField,ShowIf(nameof(_showAbility))] private StatusEffectUpgradeConfig[] _statusEffectUpgrades;
    [SerializeField,ShowIf(nameof(_showAbility))] private AbilitySO[] _abilitiesToUpgrade;
    private bool _showAbility =>  _upgradePassiveAbility || _upgradeAbility;
    public StatValueUpgradeConfig[] Stats => _stats;
    public AbilityStatUpgradeConfig[] AbilityStats => _abilityStats;
    public bool UpgradeAbility => _upgradeAbility;
    public bool UpgradePassiveAbility => _upgradePassiveAbility;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
    public AbilityBehavior[] AbilitiesBehaviors => _abilitiesBehaviors;
    public StatusEffectUpgradeConfig[] StatusEffectUpgrades => _statusEffectUpgrades;
}

[Serializable]
public struct StatValueUpgradeConfig
{
    [SerializeField] private StatType _statType;
    [SerializeField] private float _statValue;
    [SerializeField] private Factor _factor;

    public StatType StatType => _statType;
    public float StatValue => _statValue;
    public Factor Factor => _factor;
}
