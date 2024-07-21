using System;
using UnityEngine;

[Serializable]
public class AbilityUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private AbilityStatUpgradeConfig[] _stats;
    [SerializeField] private DamageBoostData[] _damageBoosts;
    [SerializeField] private AbilityBehavior[] _abilitiesBehaviors;
    [SerializeField] private StatusEffectUpgradeConfig[] _statusEffectUpgrades;
    [SerializeField] private AbilitySO[] _abilitiesToUpgrade;
    public AbilityStatUpgradeConfig[] Stats => _stats;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
    public AbilityBehavior[] AbilitiesBehaviors => _abilitiesBehaviors;
    public StatusEffectUpgradeConfig[] StatusEffectUpgrades => _statusEffectUpgrades;
}

[Serializable]
public struct AbilityStatUpgradeConfig
{
    [SerializeField] private AbilityStatType _statType;
    [SerializeField] private float _statValue;
    [SerializeField] private Factor _factor;

    public AbilityStatType StatType => _statType;
    public float StatValue => _statValue;
    public Factor Factor => _factor;
}

public enum AbilityBehavior
{
    OverhealExcessHealing,
    HealOnFullHealth,
    
}


