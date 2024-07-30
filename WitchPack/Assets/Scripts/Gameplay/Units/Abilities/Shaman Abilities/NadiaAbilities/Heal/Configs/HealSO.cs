using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal.Configs
{
    [CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/Heal/Heal")]
    public class HealSO : CastingAbilitySO
    {
        [SerializeField] protected int healAmount;
        public int HealAmount => healAmount;
    }
}
