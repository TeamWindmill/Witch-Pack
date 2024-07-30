using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Enemy.EnemyAIBehavior.GroundEnemies.States;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.JavanAbilities.Charm
{
    [CreateAssetMenu(fileName = "Charm", menuName = "Ability/Javan/Charm")]
    public class CharmSO : CastingAbilitySO
    {
        [SerializeField] private Charmed _charmedState;
        public Charmed CharmedState => _charmedState;
    }
}