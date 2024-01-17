
public class ShamanAnimator : UnitAnimator
{
    private bool _movementChange;

    protected override void MoveAnimation()
    {
        if (unit.Movement.IsMoving != _movementChange)
        {
            _movementChange = unit.Movement.IsMoving; 
            animator.SetBool("Walking", _movementChange);
        }
    }
}
