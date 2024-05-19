using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityUpgrade", menuName = "Ability/AbilityUpgrade")]
public class AbilityStatUpgrade : ScriptableObject
{
    [SerializeField] private AbilitySO _abilityToUpgrade;
    [SerializeField] private AbilityStatType _statType;
    [SerializeField] private float _abilityStatValue;

    public AbilitySO AbilityToUpgrade => _abilityToUpgrade;
    public float AbilityStatValue => _abilityStatValue;
    public AbilityStatType StatType => _statType;
}
