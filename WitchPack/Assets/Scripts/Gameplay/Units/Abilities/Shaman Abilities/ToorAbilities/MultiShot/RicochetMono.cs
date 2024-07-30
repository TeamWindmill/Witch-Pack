using System.Collections.Generic;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.MultiShot.Configs;
using GameTime;
using Tools.Targeter;
using UnityEngine;

namespace Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.MultiShot
{
    public class RicochetMono : MultiShotMono
    {
    
        private Ricochet _ricochet;
        private Enemy.Enemy _bounceTarget;
        private List<Enemy.Enemy> _bouncedTargets = new ();
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
                Enemy.Enemy target = collision.GetComponent<Enemy.Enemy>();
                if (!ReferenceEquals(target, null) && ReferenceEquals(target,_bounceTarget))
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

        protected override void OnTargetHit(Enemy.Enemy target)
        {
            if (_bounceCounter < _bounceAmount)
            {
                _bounceCounter++;
                target.Damageable.GetHit(_caster.DamageDealer, _ability);
                _bouncedTargets.Add(target);
                var availableTargets = TargetingHelper<Enemy.Enemy>.GetAvailableTargets(transform.position,_ricochet.Config.BounceRange,_ricochet.Config.TargetingLayer);
                _bounceTarget = TargetingHelper<Enemy.Enemy>.GetTarget(availableTargets, _ricochet.Config.RicochetTargetData,_bouncedTargets,transform);
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
}
