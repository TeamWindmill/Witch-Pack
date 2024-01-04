using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]

public class EnemyConfig : ScriptableObject
{
    [SerializeField] private StatSheet baseStats;

    public StatSheet BaseStats { get => baseStats; }
}
