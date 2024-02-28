using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement 
{
    public bool IsMoving => _isMoving;

    private float dstTravelled;
    private bool _isMoving;
    private bool _isActive;
    private Enemy _enemy;
    private UnitMovement _unitMovement;
    private PathCreator _path;

    public EnemyMovement(Enemy enemy)
    {
        _enemy = enemy;
        _unitMovement = enemy.Movement;
        _path = enemy.EnemyConfig.Path;
    }
    
    public void FollowPath()
    {
        dstTravelled += _enemy.Stats.MovementSpeed * GAME_TIME.GameDeltaTime;
        _enemy.transform.position = _path.path.GetPointAtDistance(dstTravelled, EndOfPathInstruction.Stop);
    }

    public void ReturnToPath()
    {
        var returnPoint = _path.path.GetClosestPointOnPath(_enemy.transform.position);
        _unitMovement.SetDestination(returnPoint);
        dstTravelled = _path.path.GetClosestDistanceAlongPath(returnPoint);
    }
}
