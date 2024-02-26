
public class EnemyAnimator : UnitAnimator
{
    private Enemy _enemy;

    public override void Init(BaseUnit unit)
    {
        base.Init(unit);
        _enemy = unit as Enemy;
        _enemy.UnitVisual.OnSpriteFlip += FlipAnimations;
    }

    protected override void MoveAnimation()
    {
        //not sure if we want an enemy move animation
    }

    protected override void DeathAnimation()
    {
        base.DeathAnimation();
        _enemy.EnemyMovement?.ToggleActive(false);
        _enemy.HpBar.gameObject.SetActive(false);
    }
    
}
