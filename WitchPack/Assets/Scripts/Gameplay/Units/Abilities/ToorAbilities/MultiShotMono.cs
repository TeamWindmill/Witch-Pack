using System;
using System.Collections.Generic;
using Tools.Helpers;
using UnityEngine;

public class MultiShotMono : InitializedMono<BaseUnit,Vector2>
{
    [SerializeField] private int numOfPoint;
    private Vector3 offset;
    private const float DistanceToTarget = 1;
    private Vector3 initialPosition;
    private List<Vector3> allPositions;
    private int counter = 0;
    
    private float _speed;
    private float _curveSpeed;
    private BaseUnit _target;

    public override void Init(BaseUnit target,Vector2 dir)
    {
        _target = target;
        Rotate(dir);
        base.Init(target,dir);
    }

    private void Update()
    {
        if(!Initialized) return;
        
        transform.Translate(_speed * GAME_TIME.GameDeltaTime * transform.forward);
        transform.LookAt(_curveSpeed * GAME_TIME.GameDeltaTime * _target.transform.position);
    }
    // public void Fire(BaseUnit shooter, OffensiveAbility givenAbility, Vector2 dir, BaseUnit target, Vector3 archSize)
    // {
    //     owner = shooter;
    //     ability = givenAbility;
    //     this.target = target;
    //     offset = archSize;
    //     if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
    //     {
    //         offset = new Vector2(0, offset.y * 2);
    //     }
    //     else
    //     {
    //         offset = new Vector2(offset.x * 2, 0);
    //     }
    //     Rotate(dir);
    //     Init();
    // }
    //
    // private void Init()
    // {
    //     initialPosition = transform.position;
    //     allPositions = new List<Vector3>(numOfPoint);
    //     Initiallized = true;
    //
    //     for (var i = 0; i < numOfPoint; i++)
    //     {
    //         var newPosition = CubicCurve(initialPosition, initialPosition + offset, initialPosition + offset,
    //             target.transform.position, (float)i / numOfPoint);
    //         allPositions.Add(newPosition);
    //     }
    //     Initiallized = true;
    // }
    
    protected void Rotate(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
