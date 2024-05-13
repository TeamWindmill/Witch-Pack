using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityUpgrade", menuName = "Ability/AbilityUpgrade")]
public class AbilityStatUpgrade : ScriptableObject
{
    [SerializeField] private AbilitySO _parentAbilitySo;
    [SerializeField] private StatValueType _statValueType;
    
    [SerializeField,ShowIf(nameof(_statValueType),StatValueType.Int)] private AbilityStatInt _abilityStatInt;
    [SerializeField,ShowIf(nameof(_statValueType),StatValueType.Float)] private AbilityStatFloat _abilityStatFloat;

    public float Value
    {
        get
        {
            switch (_statValueType)
            {
                case StatValueType.Int:
                    return _abilityStatInt.StatValue;
                case StatValueType.Float:
                    return _abilityStatInt.StatValue;
                default:
                    throw new Exception("Wrong Type");
            }
        }
    }
}
public enum StatValueType
{
    Int,
    Float
}