using System;
using UnityEngine;

[Serializable]
public class AbilityUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private AbilityStatConfig[] _stats;
    [SerializeField] private DamageBoostData[] _damageBoosts;
    [SerializeField] private AbilityBehavior[] _abilitiesBehaviors;
    [SerializeField] private AbilitySO[] _abilitiesToUpgrade;
    public AbilityStatConfig[] Stats => _stats;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
    public AbilityBehavior[] AbilitiesBehaviors => _abilitiesBehaviors;
}

[Serializable]
public struct AbilityStatConfig
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
    Test
}


