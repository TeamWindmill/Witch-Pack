using System.Collections.Generic;
using Sirenix.OdinInspector;
using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyAIConfig",fileName = "EnemyAIConfig")]
public class EnemyAIConfig : ScriptableObject
{
    [SerializeField, BoxGroup("EnemyAI")] private TargetData _targetData;
    [SerializeField, BoxGroup("EnemyAI"),Range(0,1)] private float _agroChance;
    [SerializeField, BoxGroup("EnemyAI"),Range(0,1)] private float _returnChance;
    [SerializeField, BoxGroup("EnemyAI")] private float _distanceModifier; 
    [SerializeField, BoxGroup("EnemyAI")] private List<State<EnemyAI>> _enemyStates;

    public TargetData TargetData => _targetData;
    public float AgroChance => _agroChance;
    public float ReturnChance => _returnChance;
    public float DistanceModifier => _distanceModifier;
    public List<State<EnemyAI>> EnemyStates => _enemyStates;
}