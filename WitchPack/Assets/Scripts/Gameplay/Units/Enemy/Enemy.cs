using System;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField, TabGroup("Visual")] private EnemyAnimator enemyAnimator;
    private PathCreator _path;
    private int _coreDamage;
    //testing 
    public int Id => gameObject.GetHashCode();
    
    private EnemyConfig enemyConfig;
    private int pointIndex;
    private float dstTravelled;
    private bool _isMoving;
    public EnemyConfig EnemyConfig { get => enemyConfig; }
    public int CoreDamage => _coreDamage;
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
        _coreDamage = enemyConfig.CoreDamage;
        Targeter.SetRadius(Stats.BonusRange);
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

    protected override void OnDisable()
    {
        base.OnDisable();
        Movement.OnDestenationReached -= SetNextDest;
        dstTravelled = 0;
    }

    
}
