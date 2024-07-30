using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Abilities.Auto_Attack.Configs
{
    [CreateAssetMenu(fileName = "EnemyRangedAutoAttack", menuName = "Ability/AutoAttack/EnemyRangedAutoAttack")]
    public class EnemyRangedAutoAttackSO : RangedAutoAttackSO
    {
        [BoxGroup("Auto Attack")][SerializeField] private int _coreDamage;
        public int CoreDamage => _coreDamage;
    
    }
}