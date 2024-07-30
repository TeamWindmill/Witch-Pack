using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Enemy.EnemyAIBehavior.GroundEnemies.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.RockMonolith.Configs
{
    [CreateAssetMenu(menuName = "Ability/Lila/RockMonolith/RockMonolith",fileName = "RockMonolith")]
    public class RockMonolithSO : OffensiveAbilitySO
    {
        [BoxGroup("Rock Monolith"),SerializeField] private float _duration;
        [BoxGroup("Rock Monolith"),SerializeField] private int _damageIncreasePerHit;
        [BoxGroup("Rock Monolith"),SerializeField] private Taunt _tauntState;
        [BoxGroup("Rock Monolith"),SerializeField,Tooltip("minimum enemies in range in order to cast the ability")] private float _minEnemiesForTaunt;

        public int DamageIncreasePerHit => _damageIncreasePerHit;
        public float Duration => _duration;
        public Taunt TauntState => _tauntState;
        public float MinEnemiesForTaunt => _minEnemiesForTaunt;
    }
}