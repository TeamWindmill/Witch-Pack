using System.Collections.Generic;
using Sirenix.OdinInspector;
using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/GroundEnemyAIConfig",fileName = "GroundEnemyAIConfig")]
public class GroundEnemyAIConfig : EnemyAIConfig
{
    [SerializeField, BoxGroup("EnemyAI"),Range(0,1)] private float _agroChance;
    [SerializeField, BoxGroup("EnemyAI"),Range(0,1)] private float _returnChance;
    [SerializeField, BoxGroup("EnemyAI")] private float _distanceModifier; 

    public float AgroChance => _agroChance;
    public float ReturnChance => _returnChance;
    public float DistanceModifier => _distanceModifier;
}