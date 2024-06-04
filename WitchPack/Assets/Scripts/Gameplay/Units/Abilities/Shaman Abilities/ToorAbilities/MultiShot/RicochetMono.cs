using System.Collections.Generic;
using System.Linq;
using Tools.Targeter;
using UnityEngine;

public class RicochetMono : MultiShotMono
{
    
    private Ricochet _ricochet;
    private Enemy _bounceTarget;
    private List<Enemy> _bouncedTargets = new ();
    private bool _bouncing = false;
    private int _bounceCounter = 0;
    private int _bounceAmount => (int)_ricochet.GetAbilityStatValue(AbilityStatType.BounceAmount);


    public override void Init(MultiShotType type, BaseUnit caster, BaseUnit target, MultiShot ability, float angle)
    {
        base.Init(type,caster, target, ability, angle);
        _bouncing = false;
        _bounceCounter = 0;
        _ricochet = ability as Ricochet;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bounceCounter >= _bounceAmount)
        {
            Enemy target = collision.GetComponent<Enemy>();
            if (!ReferenceEquals(target, null))
            {
                target.Damageable.GetHit(_caster.DamageDealer, _ability);
                Disable();
            }
        }
        else
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    protected override void OnTargetHit(Enemy target)
    {
        if (_bounceCounter < _bounceAmount)
        {
            _bounceCounter++;
            target.Damageable.GetHit(_caster.DamageDealer, _ability);
            _bouncedTargets.Add(target);
            var availableTargets = TargetingHelper<Enemy>.GetAvailableTargets(transform.position,_ricochet.Config.BounceRange,_ricochet.Config.TargetingLayer);
            _bounceTarget = TargetingHelper<Enemy>.GetTarget(availableTargets, _ricochet.Config.RicochetTargetData,_bouncedTargets,transform);
            if(!ReferenceEquals(_bounceTarget,null)) _bouncing = true;
            else Disable();
        }
        else base.OnTargetHit(target);
    }

    protected override void FixedUpdate()
    {
        if (_bouncing)
        {
            _rb.velocity = _ricochet.Config.BounceSpeed * GAME_TIME.TimeRate * transform.up;
            
            if(!Launched) return;
            var dir = _rb.position - (Vector2)_targetPos;
            dir.Normalize();
            float rotateAmount = Vector3.Cross(dir,transform.up).z;

            _rb.angularVelocity = rotateAmount * _ricochet.Config.BounceCurveSpeed * GAME_TIME.TimeRate;

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
