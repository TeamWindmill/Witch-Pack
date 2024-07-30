using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseConfigs.Passives
{
    [CreateAssetMenu(fileName = "StatPassive", menuName = "Ability/Passive/Stat")]
    public class StatPassiveSO : PassiveSO
    {
        [SerializeField] protected StatValue[] statIncreases;
        public StatValue[] StatIncreases => statIncreases;
    }

    [System.Serializable]
    public struct StatValue
    {
        public StatType StatType;
        public float Value;
    }
}