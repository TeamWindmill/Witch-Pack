using System;
using System.Collections.Generic;
using Tools.Helpers;
using UnityEngine;

public class MultiShotMono : MonoBehaviour
{
    public bool Launched { get; private set; }

    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _curveSpeed;
    [SerializeField] private float _Delay;
    private BaseUnit _target;
    protected Vector3 _targetPos;
    protected BaseUnit _caster;
    protected OffensiveAbility _ability;

    protected static readonly float HIT_POS_OFFSET = 0.8f;

    public virtual void Init(MultiShotType type,BaseUnit caster, BaseUnit target,OffensiveAbility offensiveAbility,float angle)
    {
        _caster = caster;
        _target = target;
        _targetPos = target.transform.position;
        _ability = offensiveAbility;
        //need to add switch case for enabling different prefabs
        transform.rotation = Quaternion.Euler(0,0,angle);
        TimerManager.Instance.AddTimer(_Delay, () => Launched = true, true);
    }

    protected virtual void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed;
        
        if(!Launched) return;
        var dir = _rb.position - (Vector2)_targetPos;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir,transform.up).z;

        _rb.angularVelocity = rotateAmount * _curveSpeed;

        if (!_target.IsDead)
        {
            _targetPos = _target.transform.position;
            return;
        }
        if(Vector3.Distance(transform.position, _targetPos) < HIT_POS_OFFSET) Disable();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy target = collision.GetComponent<Enemy>();
        if (!ReferenceEquals(target, null) && ReferenceEquals(target, _target))
        {
            OnTargetHit(target);
        }
    }

    protected virtual void OnTargetHit(Enemy target)
    {
        target.Damageable.GetHit(_caster.DamageDealer, _ability);
        Disable();
    }

    protected void Disable()
    {
        _caster = null;
        _ability = null;
        _target = null;
        Launched = false;
        gameObject.SetActive(false);
    }
}
