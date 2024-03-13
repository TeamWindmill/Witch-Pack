using System.Collections.Generic;
using System.Linq;
using Tools.Targeter;
using UnityEngine;

public class RicochetMono : MultiShotMono
{
    
    private RicochetSO _ricochetSO;
    private Enemy _bounceTarget;
    private bool _bouncing;


    public override void Init(MultiShotType type, BaseUnit caster, BaseUnit target, OffensiveAbility offensiveAbility, float angle)
    {
        base.Init(type,caster, target, offensiveAbility, angle);
        if(offensiveAbility is not RicochetSO ricochetSO) return;
        _ricochetSO = ricochetSO;
    }

    protected override void OnTargetHit(Enemy target)
    {
        if (_bouncing)
        {
            target.Damageable.GetHit(_caster.DamageDealer, _ability);
            var availableTargets = TargetingHelper<Enemy>.GetAvailableTargets(transform.position,_ricochetSO.BounceRange,_ricochetSO.TargetingLayer);
            _bounceTarget = TargetingHelper<Enemy>.GetTarget(availableTargets, _ricochetSO.RicochetTargetData,new []{target}.ToList(),transform);
            if(!ReferenceEquals(_bounceTarget,null)) _bouncing = true;
        }
        else base.OnTargetHit(target);
    }

    protected override void FixedUpdate()
    {
        if (_bouncing)
        {
            _rb.velocity = transform.up * _ricochetSO.BounceSpeed;
        
            if(!Launched) return;
            var dir = _rb.position - (Vector2)_targetPos;
            dir.Normalize();
            float rotateAmount = Vector3.Cross(dir,transform.up).z;

            _rb.angularVelocity = rotateAmount * _ricochetSO.BounceCurveSpeed;

            if (!_bounceTarget.IsDead)
            {
                _targetPos = _bounceTarget.transform.position;
                return;
            }
            if(Vector3.Distance(transform.position, _targetPos) < HIT_POS_OFFSET) Disable();
        }
        else base.FixedUpdate();
    }
}
