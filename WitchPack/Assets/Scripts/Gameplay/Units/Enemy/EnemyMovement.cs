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
    private Enemy _enemy;
    private UnitMovement _unitMovement;
    private PathCreator _path;

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _unitMovement = enemy.Movement;
        _path = enemy.EnemyConfig.Path;
        ToggleMove(true);
    }
    
    public void FollowPath()
    {
        if(!_isMoving) return;
        dstTravelled += _enemy.Stats.MovementSpeed * GAME_TIME.GameDeltaTime;
        _enemy.transform.position = _path.path.GetPointAtDistance(dstTravelled, EndOfPathInstruction.Stop);
        
    }

    public void ReturnToPath(Vector3 currentPos)
    {
        var returnPoint = _path.path.GetClosestPointOnPath(currentPos);
        _unitMovement.SetDest(returnPoint);
        _unitMovement.OnDestenationReached
        dstTravelled = _path.path.GetClosestDistanceAlongPath(currentPos);
        
    }
    
    public void ToggleMove(bool state)
    {
        _isMoving = state;
    }

    public void OnDisable()
    {
        dstTravelled = 0;
    }
}
