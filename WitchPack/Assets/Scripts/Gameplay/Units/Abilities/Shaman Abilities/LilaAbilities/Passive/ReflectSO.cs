using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs.Passives;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.Passive
{
    [CreateAssetMenu(menuName = "Ability/Lila/Passive/Reflect",fileName = "Reflect")]
    public class ReflectSO : PassiveSO
    {
        [SerializeField] private float _reflectedDamagePercent;

        public float ReflectedDamagePercent => _reflectedDamagePercent;
    }
}