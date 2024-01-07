using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private ShamanTargeter shamanTargeter;
    private CustomPath givenPath;

    public override StatSheet BaseStats => enemyConfig.BaseStats;
    
    public override void Init(BaseUnitConfig givenConfig)
    {
        enemyConfig = givenConfig as EnemyConfig;
        base.Init(givenConfig);
        shamanTargeter.SetRadius(Stats.BonusRange);
        //givenPath = levelmanager.GetPath();
        //movement.setdest givenPath[0];
    }

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public ShamanTargeter ShamanTargeter { get => shamanTargeter; }
}
