using System;
using System.Collections.Generic;
using Tools.Helpers;
using UnityEngine;

public class MultiShotMono : InitializedMono<BaseUnit,OffensiveAbility,Vector3>
{
    [SerializeField] private int numOfPoint;
    private Vector3 offset;
    private const float DistanceToTarget = 1;
    private Vector3 initialPosition;
    private List<Vector3> allPositions;
    private int counter = 0;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _curveSpeed;
    private BaseUnit _target;

    public override void Init(BaseUnit target,OffensiveAbility offensiveAbility,Vector3 dir)
    {
        _target = target;
        transform.Rotate(Quaternion.Euler(dir).eulerAngles);
        base.Init(target,offensiveAbility,dir);
    }

    private void FixedUpdate()
    {
        if(!Initialized) return;

        var dir = (Vector2)_target.transform.position - _rb.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir,transform.forward).z;

        _rb.angularVelocity = -rotateAmount * _curveSpeed;

        _rb.velocity = transform.up * _speed;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // BaseUnit target = collision.GetComponent<BaseUnit>();
        // if (!ReferenceEquals(target, null) && ReferenceEquals(target, this.target))
        // {
        //     target.Damageable.GetHit(owner.DamageDealer, ability);
        //     OnShotHit?.Invoke(ability, owner, target);
        //     Disable();
        // }
    }
    public virtual void Disable()
    {
        // owner = null;
        // ability = null;
        // SetSpeed(initialSpeed);
        // OnShotHit?.RemoveAllListeners();
        // gameObject.SetActive(false);
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
