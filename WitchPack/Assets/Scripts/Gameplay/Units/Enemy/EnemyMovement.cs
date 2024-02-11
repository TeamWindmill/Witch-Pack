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
        ToggleMove(true);
        ToggleActive(true);
    }
    
    public void FollowPath()
    {
        if(!_isMoving || !_isActive) return;
        dstTravelled += _enemy.Stats.MovementSpeed * GAME_TIME.GameDeltaTime;
        _enemy.transform.position = _path.path.GetPointAtDistance(dstTravelled, EndOfPathInstruction.Stop);
    }

    public void ReturnToPath(Vector3 currentPos)
    {
        if(!_isActive) return;
        var returnPoint = _path.path.GetClosestPointOnPath(currentPos);
        _unitMovement.SetDest(returnPoint);
        dstTravelled = _path.path.GetClosestDistanceAlongPath(returnPoint);
        _unitMovement.OnDestinationReached += ContinuePath;
    }

    private void ContinuePath()
    {
        if(!_isActive) return;
        _isMoving = true;
        _unitMovement.ToggleMovement(false);
    }
    
    public void ToggleMove(bool state)
    {
        _isMoving = state;
    }
    public void ToggleActive(bool state)
    {
        _isActive = state;
    }
}
