using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct AbilityStatUpgrade
{
    public AbilitySO[] AbilitiesToUpgrade;
    
    [SerializeField] private string _name;
    [SerializeField] private AbilityStatType _statType;
    [SerializeField] private float _abilityStatValue;
    [SerializeField] private Factor _factor;

    public float AbilityStatValue => _abilityStatValue;
    public AbilityStatType StatType => _statType;
    public string Name => _name;
    public Factor Factor => _factor;
}

public enum Factor
{
    Add,
    Subtract,
}
