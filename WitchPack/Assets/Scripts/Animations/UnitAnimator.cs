using System;
using UnityEngine;

public abstract class UnitAnimator : MonoBehaviour
{
    private Animator _animator;
    private BaseUnit _unit;
    
    public BaseUnit Unit => _unit;


    private bool _movementChange;
    private bool _isFlipped;
    public virtual void Init(BaseUnit unit, Animator animator)
    {
        _unit = unit;
        _animator = animator;
        unit.Damageable.OnHitGFX += GetHitAnimation;
        unit.Damageable.OnDeathGFX += DeathAnimation;
        unit.UnitVisual.OnSpriteFlip += FlipAnimations;
        //onattack
    }

    protected abstract void MoveAnimation();
    protected virtual void AttackAnimation()
    {
        _animator.SetTrigger(_isFlipped ? "Attack_Flipped" : "Attack");
    }

    protected virtual void DeathAnimation()
    {
        _unit.Movement.SetSpeed(0);
        _animator.SetBool("Dead",true);
        _animator.SetTrigger(_isFlipped ? "Death_Flipped" : "Death");
    }

    protected virtual void GetHitAnimation(bool isCrit)
    {
        _animator.SetTrigger(_isFlipped ? "GetHit_Flipped" : "GetHit");
    }

    protected virtual void FlipAnimations(bool state)
    {
        _isFlipped = state;
    }

    

    private void Update()
    {
        MoveAnimation();
    }
}
