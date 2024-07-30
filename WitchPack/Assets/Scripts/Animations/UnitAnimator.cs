using System;
using Gameplay.Units;
using UnityEngine;

namespace Animations
{
    public abstract class UnitAnimator : MonoBehaviour
    {
        public event Action OnDeathAnimationEnd;
        [SerializeField] protected Animator animator;
        protected BaseUnit unit;
        private bool _initialized;

        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
        }


        private bool _isFlipped;
        private static readonly int Death = Animator.StringToHash("Dead");

        public virtual void Init(BaseUnit unit)
        {
            this.unit = unit;
            this.unit.Damageable.OnHitGFX += GetHitAnimation;
            this.unit.Damageable.OnDeathGFX += DeathAnimation;
            this.unit.AbilityHandler.AutoAttackCaster.OnAttack += AttackAnimation;
            animator.SetBool(Death,false);
            _initialized = true;
        }

        protected abstract void MoveAnimation();

        protected virtual void AttackAnimation()
        {
            animator.SetTrigger(_isFlipped ? "Attack_Flipped" : "Attack");
        }

        protected virtual void DeathAnimation()
        {
            unit.OnDeathAnimation();
            animator.SetBool(Death,true);
            animator.SetTrigger(_isFlipped ? "Death_Flipped" : "Death");
        }
        public virtual void DeathAnimationEnded()
        {
            OnDeathAnimationEnd?.Invoke();
        }

        protected virtual void GetHitAnimation(bool isCrit)
        {
            animator.SetTrigger(_isFlipped ? "GetHit_Flipped" : "GetHit");
        }

        public void FlipAnimations(bool state)
        {
            _isFlipped = state;
        }
        private void Update()
        {
            if(!_initialized) return;
            MoveAnimation();
        }
    
    }
}
