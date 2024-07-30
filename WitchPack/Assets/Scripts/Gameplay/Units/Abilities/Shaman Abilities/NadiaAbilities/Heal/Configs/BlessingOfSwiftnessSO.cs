using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal.Configs
{
    [CreateAssetMenu(fileName = "ability", menuName = "Ability/Nadia/Heal/BlessingOfSwiftness")]
    public class BlessingOfSwiftnessSO : HealSO
    {
        [SerializeField] private StatusEffectConfig attackSpeedBoost;
        public StatusEffectConfig AttackSpeedBoost => attackSpeedBoost;
    }
}
