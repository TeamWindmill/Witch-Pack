using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField] private EnemyConfig enemyConfig;

    public override StatSheet BaseStats => enemyConfig.BaseStats;

    public EnemyConfig EnemyConfig { get => enemyConfig; }
}
