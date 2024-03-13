using System;
using System.Collections.Generic;
using Tools.Helpers;
using UnityEngine;

public class MultiShotMono : MonoBehaviour
{
    public bool Launched { get; private set; }

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _curveSpeed;
    [SerializeField] private float _Delay;
    private BaseUnit _target;
    private Vector3 _targetPos;
    private BaseUnit _caster;
    private OffensiveAbility _ability;

    private static readonly float HIT_POS_OFFSET = 0.8f;

    public void Init(BaseUnit caster, BaseUnit target,OffensiveAbility offensiveAbility,float angle)
    {
        _caster = caster;
        _target = target;
        _targetPos = target.transform.position;
        _ability = offensiveAbility;
        transform.rotation = Quaternion.Euler(0,0,angle);
        TimerManager.Instance.AddTimer(_Delay, () => Launched = true, true);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed;
        
        if(!Launched) return;
        if(Vector3.Distance(transform.position, _targetPos) < HIT_POS_OFFSET) Disable();
        var dir = _rb.position - (Vector2)_targetPos;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir,transform.up).z;

        _rb.angularVelocity = rotateAmount * _curveSpeed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        BaseUnit target = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(target, null) && ReferenceEquals(target, _target))
        {
            target.Damageable.GetHit(_caster.DamageDealer, _ability);
            Disable();
        }
    }
    private void Disable()
    {
        _caster = null;
        _ability = null;
        _target = null;
        Launched = false;
        gameObject.SetActive(false);
    }
}
