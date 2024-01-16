using System;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    [SerializeField] private ShamanTargeter shamanTargeter;
    private PathCreator givenPath;
    //testing 
    private EnemyConfig enemyConfig;
    private int pointIndex = 0;
    private float dstTravelled;
    private int _speed = 5;

    public override StatSheet BaseStats => enemyConfig.BaseStats;
    private void OnValidate()
    {
        enemyAnimator ??= GetComponentInChildren<EnemyAnimator>();
    }
    public override void Init(BaseUnitConfig givenConfig)
    {
        pointIndex = 0;
        enemyConfig = givenConfig as EnemyConfig;
        base.Init(enemyConfig);
        shamanTargeter.SetRadius(Stats.BonusRange);
        //Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        //Movement.OnDestenationReached += SetNextDest;
        enemyAnimator.Init(this);
    }

    public void SetPath(PathCreator path)
    {
        givenPath = path;
    }

    private void Update()
    {
        dstTravelled = _speed * Time.deltaTime;
        transform.position = givenPath.path.GetPointAtDistance(dstTravelled, EndOfPathInstruction.Stop);
    }

    private void SetNextDest()
    {
        //pointIndex++;
        // if (givenPath.Waypoints.Count <= pointIndex)//if reached the end of the path target nexus 
        // {
        //     gameObject.SetActive(false);
        // }
        // else
        // {
        //     Debug.Log("set dest");
        //     Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        // }

    }

    private void OnDisable()
    {
        Movement.OnDestenationReached -= SetNextDest;
    }

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public ShamanTargeter ShamanTargeter { get => shamanTargeter; }
}
