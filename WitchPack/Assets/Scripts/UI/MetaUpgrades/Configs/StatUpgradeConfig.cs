using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "MetaUpgrade/StatUpgrade",fileName = "StatUpgrade")]
[Serializable]
public class StatUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private bool _upgradeAbility;
    [SerializeField,HideIf(nameof(_upgradeAbility))] private StatValueUpgradeConfig[] _stats;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilityStatConfig[] _abilityStats;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilityBehavior[] _abilitiesBehaviors;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private DamageBoostData[] _damageBoosts;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilitySO[] _abilitiesToUpgrade;

    public StatValueUpgradeConfig[] Stats => _stats;
    public AbilityStatConfig[] AbilityStats => _abilityStats;
    public bool UpgradeAbility => _upgradeAbility;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
    public AbilityBehavior[] AbilitiesBehaviors => _abilitiesBehaviors;
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
