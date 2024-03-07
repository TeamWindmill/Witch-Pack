
using UnityEngine;

public class EnemyVisualHandler : UnitVisualHandler
{
    private Enemy _enemy;

    [SerializeField] private PoisonIvyVisuals poisonIvyVisuals;

    public PoisonIvyVisuals PoisonIvyVisuals { get => poisonIvyVisuals; }

    public override void Init(BaseUnit unit, BaseUnitConfig config)
    {
        base.Init(unit, config);
        _enemy = unit as Enemy;
    }

    protected override void FlipSpriteOnTarget(BaseUnit target)
    {
        if (_enemy.EnemyAI.ActiveState is FollowPath) return;
        base.FlipSpriteOnTarget(target);
    }

    protected override void OnUnitDeath()
    {
        Color color = Color.white;
        color.a = 1;
        spriteRenderer.color = color;
        animator.gameObject.transform.localScale = Vector3.one;
        _enemy.gameObject.SetActive(false);
    }
}