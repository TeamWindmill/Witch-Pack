using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Rooting_Vines.Configs
{
    [CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/RootingVines/HealingWeeds")]
    public class HealingWeedsSO : OffensiveAbilitySO
    {
        [SerializeField] private float aoeScale = 1;
        [SerializeField] private float lastingTime;
        [SerializeField] private StatusEffectConfig[] healStatusEffects;
        public float AoeScale => aoeScale;
        public float LastingTime => lastingTime;

        public StatusEffectConfig[] HealStatusEffects => healStatusEffects;
    }
}
