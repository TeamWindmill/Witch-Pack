using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Abilities.Enemy_Abilities.Blaster
{
    [CreateAssetMenu(fileName = "Fireball", menuName = "Ability/EnemyAbilities/Fireball")]
    public class FireballSO : OffensiveAbilitySO
    {
        [BoxGroup("Fireball")][SerializeField] private float _speed;
        [BoxGroup("AOE Fire")][SerializeField] private float _duration;
        [BoxGroup("AOE Fire")][SerializeField] private float _tickTime;
        [BoxGroup("AOE Fire")][SerializeField] private int _tickAmount;
        [BoxGroup("AOE Fire")][SerializeField] private float _aoeScale;
        [BoxGroup("AOE Fire")][SerializeField] private int _burnDamage;

        public float Duration => _duration;
        public float Speed => _speed;
        public float TickTime => _tickTime;
        public int TickAmount => _tickAmount;
        public float AoeScale => _aoeScale;
        public int BurnDamage => _burnDamage;
    }
}