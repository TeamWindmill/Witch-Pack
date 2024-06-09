using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "MetaUpgrade/StatUpgrade",fileName = "StatUpgrade")]
[Serializable]
public class StatUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private bool _upgradeAbility;
    [SerializeField,HideIf(nameof(_upgradeAbility))] private StatType _statType;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilityStatType _abilityStatType;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private AbilitySO[] _abilitiesToUpgrade;
    [SerializeField,ShowIf(nameof(_upgradeAbility))] private DamageBoostData[] _damageBoosts;

    public StatType StatType => _statType;
    public AbilityStatType AbilityStatType => _abilityStatType;
    public bool UpgradeAbility => _upgradeAbility;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
}