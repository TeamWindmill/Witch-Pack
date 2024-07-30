using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseConfigs.Passives
{
    [CreateAssetMenu(fileName = "ConditionalStatPassive", menuName = "Ability/Passive/ConditionalStat")]
    public class ConditionalStatPassiveSO : StatPassiveSO
    {
        [SerializeField] private StatusEffectConfig _conditionalStatusEffect;
        public StatusEffectConfig ConditionalStatusEffect => _conditionalStatusEffect;
    }
}
