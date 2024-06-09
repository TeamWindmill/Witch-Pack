using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "MetaUpgrade/StatUpgrade",fileName = "StatUpgrade")]
[Serializable]
public class StatUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private bool _upgradeAbility;
    [SerializeField,HideIf(nameof(_upgradeAbility))] private StatValueUpgradeConfig[] _stats;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilityStatType _abilityStatType;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilitySO[] _abilitiesToUpgrade;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private DamageBoostData[] _damageBoosts;

    public StatValueUpgradeConfig[] Stats => _stats;
    public AbilityStatType AbilityStatType => _abilityStatType;
    public bool UpgradeAbility => _upgradeAbility;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
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
