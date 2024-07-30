using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using UnityEngine;

namespace Gameplay.Units.Abilities.Auto_Attack.Configs
{
    [CreateAssetMenu(fileName = "EnemyMeleeAutoAttack", menuName = "Ability/AutoAttack/MeleeAutoAttack")]
    public class EnemyMeleeAutoAttackSO : OffensiveAbilitySO
    {
        [SerializeField] private float _meleeRange;

        public float MeleeRange => _meleeRange;
    }
}