using System;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    [SerializeField] private ShamanTargeter shamanTargeter;
    private PathCreator _path;
    //testing 
    private EnemyConfig enemyConfig;
    private int pointIndex;
    private float dstTravelled;
    private bool _isMoving;

    public bool IsMoving => _isMoving;
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
        _path = enemyConfig.Path;
        shamanTargeter.SetRadius(Stats.BonusRange);
        //Movement.SetDest(givenPath.Waypoints[pointIndex].position);
        //Movement.OnDestenationReached += SetNextDest;
        enemyAnimator.Init(this);
        ToggleMove(true);
    }

    private void Update()
    {
        if(!_isMoving) return;
        dstTravelled += Stats.MovementSpeed * GAME_TIME.GameDeltaTime;
        transform.position = _path.path.GetPointAtDistance(dstTravelled, EndOfPathInstruction.Stop);
    }

    public void ToggleMove(bool state)
    {
        _isMoving = state;
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
        dstTravelled = 0;
    }

    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public ShamanTargeter ShamanTargeter { get => shamanTargeter; }
}
