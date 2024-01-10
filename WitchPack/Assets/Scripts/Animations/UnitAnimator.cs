using UnityEngine;

public abstract class UnitAnimator : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected BaseUnit unit;

    private void OnValidate()
    {
        animator ??= GetComponent<Animator>();
    }


    private bool _isFlipped;
    public virtual void Init(BaseUnit unit)
    {
        this.unit = unit;
        this.unit.Damageable.OnHitGFX += GetHitAnimation;
        this.unit.Damageable.OnDeathGFX += DeathAnimation;
        this.unit.UnitVisual.OnSpriteFlip += FlipAnimations;
        this.unit.AutoAttackHandler.OnAttack += AttackAnimation;
    }

    protected abstract void MoveAnimation();

    protected virtual void AttackAnimation()
    {
        animator.SetTrigger(_isFlipped ? "Attack_Flipped" : "Attack");
    }

    protected virtual void DeathAnimation()
    {
        unit.Movement.ToggleMovement(false);
        animator.SetBool("Dead",true);
        animator.SetTrigger(_isFlipped ? "Death_Flipped" : "Death");
    }

    protected virtual void GetHitAnimation(bool isCrit)
    {
        animator.SetTrigger(_isFlipped ? "GetHit_Flipped" : "GetHit");
    }

    protected virtual void FlipAnimations(bool state)
    {
        _isFlipped = state;
    }
    private void Update()
    {
        MoveAnimation();
    }
        
  /*  private void OnDestroy() //pointless?
    {
        unit.Damageable.OnHitGFX -= GetHitAnimation;
        unit.Damageable.OnDeathGFX -= DeathAnimation;
        unit.UnitVisual.OnSpriteFlip -= FlipAnimations;
        unit.AutoAttackHandler.OnAttack -= AttackAnimation;
    }*/
}
