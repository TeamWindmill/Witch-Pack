using System.Collections.Generic;
using Gameplay.Targeter;
using Sirenix.OdinInspector;
using Systems.StateMachine;
using UnityEngine;

namespace Gameplay.Units.Enemy.EnemyAIBehavior
{
    [CreateAssetMenu(menuName = "Enemy/EnemyAIConfig",fileName = "EnemyAIConfig")]
    public class EnemyAIConfig : ScriptableObject
    {
        [SerializeField, BoxGroup("EnemyAI")] private TargetData _targetData;
        [SerializeField, BoxGroup("EnemyAI")] private List<State<EnemyAI>> _enemyStates;
        public List<State<EnemyAI>> EnemyStates => _enemyStates;
        public TargetData TargetData => _targetData;
    }
}