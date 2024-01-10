
public class ShamanAnimator : UnitAnimator
{
    private bool _movementChange;

    public override void Init(BaseUnit unit)
    {
        base.Init(unit);
    }

    protected override void MoveAnimation()
    {
        if (unit.Movement.IsMoving != _movementChange)
        {
            _movementChange = unit.Movement.IsMoving; 
            animator.SetBool("Walking", _movementChange);
        }
    }
}
