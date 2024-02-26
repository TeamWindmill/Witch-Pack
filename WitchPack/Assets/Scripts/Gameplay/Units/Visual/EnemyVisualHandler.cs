
using UnityEngine;

public class EnemyVisualHandler : UnitVisualHandler
{
    private Enemy _enemy;
    public override void Init(BaseUnit unit, BaseUnitConfig config)
    {
        base.Init(unit, config);
        _enemy = unit as Enemy;
    }

    protected override void FlipSpriteOnTarget(BaseUnit target)
    {
        if (_enemy.EnemyMovement.IsMoving) return;
        base.FlipSpriteOnTarget(target);
    }

    protected override void OnUnitDeath()
    {
        Color color = Color.white;
        color.a = 1;
        spriteRenderer.color = color;
        animator.gameObject.transform.localScale = Vector3.one;
    }
}