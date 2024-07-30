using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs.Passives;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.Passive
{
    [CreateAssetMenu(fileName = "HealingLight", menuName = "Ability/Lila/Passive/HealingLight")]
    public class HealingLightSO : StatPassiveSO
    {
        [SerializeField] private float _healPercentage;
        [SerializeField] private float _healInterval;
        public float HealPercentage => _healPercentage;
        public float HealInterval => _healInterval;
    }
}