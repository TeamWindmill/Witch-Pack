
public class ShamanAnimator : UnitAnimator
{
    private bool _movementChange;

    public override void Init(BaseUnit unit)
    {
        base.Init(unit);
        //this.unit.UnitVisual.OnSpriteFlip += FlipAnimations; // TODO: Uncomment
    }

    protected override void MoveAnimation()
    {
        if (unit.Movement.IsMoving != _movementChange)
        {
            _movementChange = unit.Movement.IsMoving; 
            animator.SetBool("Walking", _movementChange);
        }
    }

    protected override void DeathAnimation()
    {
        base.DeathAnimation();
    }

    public override void DeathAnimationEnded()
    {
        base.DeathAnimationEnded();
    }
}
