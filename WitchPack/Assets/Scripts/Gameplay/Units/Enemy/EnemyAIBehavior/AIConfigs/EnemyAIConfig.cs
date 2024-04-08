using System.Collections.Generic;
using Sirenix.OdinInspector;
using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyAIConfig",fileName = "EnemyAIConfig")]
public class EnemyAIConfig : ScriptableObject
{
    [SerializeField, BoxGroup("EnemyAI")] private TargetData _targetData;
    [SerializeField, BoxGroup("EnemyAI")] private List<State<EnemyAI>> _enemyStates;
    public List<State<EnemyAI>> EnemyStates => _enemyStates;
    public TargetData TargetData => _targetData;
}