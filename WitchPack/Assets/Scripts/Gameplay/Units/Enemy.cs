using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField] private EnemyConfig enemyConfig;

    public override StatSheet BaseStats => enemyConfig.BaseStats;

    protected override void InitUnit(BaseUnitConfig givenConfig)
    {
        base.InitUnit(givenConfig);
        enemyConfig = givenConfig as EnemyConfig;

    }

    public EnemyConfig EnemyConfig { get => enemyConfig; }
}
