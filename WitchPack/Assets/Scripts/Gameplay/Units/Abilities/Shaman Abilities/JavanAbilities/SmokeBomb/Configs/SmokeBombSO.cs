using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.JavanAbilities.SmokeBomb.Configs
{
    [CreateAssetMenu(fileName = "SmokeBomb", menuName = "Ability/Javan/SmokeBomb/SmokeBomb")]
    public class SmokeBombSO : OffensiveAbilitySO
    {
        public float Duration => _duration;
        public float Size => _size;

        [SerializeField] private float _duration;
        [SerializeField] private float _size;
    }
}