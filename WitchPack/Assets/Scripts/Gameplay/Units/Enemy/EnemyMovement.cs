using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement
{
    public float DistanceRemaining => _path.path.length - _dstTravelled;
    private float _dstTravelled;
    private readonly Enemy _enemy;
    private readonly UnitMovement _unitMovement;
    private readonly PathCreator _path;

    public EnemyMovement(Enemy enemy)
    {
        _enemy = enemy;
        _unitMovement = enemy.Movement;
        _path = enemy.EnemyConfig.Path;
    }
    
    public void FollowPath()
    {
        _dstTravelled += _enemy.Stats.MovementSpeed * GAME_TIME.GameDeltaTime;
        _enemy.transform.position = _path.path.GetPointAtDistance(_dstTravelled, EndOfPathInstruction.Stop);
    }

    public void ReturnToPath()
    {
        var returnPoint = _path.path.GetClosestPointOnPath(_enemy.transform.position);
        _unitMovement.SetDestination(returnPoint);
        _dstTravelled = _path.path.GetClosestDistanceAlongPath(returnPoint);
    }
}
