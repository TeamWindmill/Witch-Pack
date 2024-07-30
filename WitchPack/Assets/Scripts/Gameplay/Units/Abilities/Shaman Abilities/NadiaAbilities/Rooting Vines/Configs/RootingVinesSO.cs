using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Rooting_Vines.Configs
{
    [CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/RootingVines/RootingVines")]

    public class RootingVinesSO : OffensiveAbilitySO
    {
        [SerializeField] private float lastingTime;
        [SerializeField] private float _aoeScale = 1;

        public float LastingTime => lastingTime;
        public float AoeScale => _aoeScale;
    }
}
