using UnityEngine;

public class EnemyVisualHandler : UnitVisualHandler
{
    private Enemy _enemy;
    public EnemyEffectHandler EnemyEffectHandler => EffectHandler as EnemyEffectHandler;

    [SerializeField] private ParticleSystem _hitEffect;

    public ParticleSystem HitEffect => _hitEffect;

    public override void Init(BaseUnit unit, BaseUnitConfig config)
    {
        base.Init(unit, config);
        _enemy = unit as Enemy;
    }

    protected override void FlipSpriteOnTarget(CastingAbility ability,IDamagable target)
    {
        if (_enemy.EnemyAI.ActiveState is FollowPath) return;
        base.FlipSpriteOnTarget(ability,target);
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