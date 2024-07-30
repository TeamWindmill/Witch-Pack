using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using UnityEngine;

namespace Gameplay.Units.Abilities.Auto_Attack.Configs
{
    public abstract class RangedAutoAttackSO : OffensiveAbilitySO
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}