using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private ShamanTargeter shamanTargeter;
    [SerializeField] private CustomPath givenPath;
    //testing 
    private int pointIndex = 0;

    public override StatSheet BaseStats => enemyConfig.BaseStats;

    private void Start() // temp
    {
        Init(enemyConfig);
    }

    public override void Init(BaseUnitConfig givenConfig)
    {
        enemyConfig = givenConfig as EnemyConfig;
        base.Init(enemyConfig);
        shamanTargeter.SetRadius(Stats.BonusRange);
        Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        Movement.OnDestenationReached += SetNextDest;
        enemyAnimator.Init(this,UnitVisual.UnitAnimator);
    }


    private void SetNextDest(Vector3 pos)
    {
        pointIndex++;
        if (givenPath.Waypoints.Count <= pointIndex)//if reached the end of the path target nexus 
        {
            return; //for now
        }
        else
        {
            Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        }

    }

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public ShamanTargeter ShamanTargeter { get => shamanTargeter; }
}
