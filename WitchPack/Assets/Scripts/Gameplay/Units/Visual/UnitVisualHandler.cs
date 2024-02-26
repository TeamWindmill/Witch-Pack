using System;
using UnityEngine;

public abstract class UnitVisualHandler : MonoBehaviour
{
    public Action<bool> OnSpriteFlip;
    public Animator Animator => animator;
    public UnitEffectHandler EffectHandler => effectHandler;
    
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] private UnitAnimator unitAnimator;
    [SerializeField] private UnitEffectHandler effectHandler;


    private Vector2 _lastPos;
    protected BaseUnit _baseUnit;

    private void Start()
    {
        unitAnimator.OnDeathAnimationEnd += OnUnitDeath;
    }

    public virtual void Init(BaseUnit unit, BaseUnitConfig config)
    {
        _baseUnit = unit;
        spriteRenderer.sprite = config.UnitSprite;
        effectHandler.Init();
        _baseUnit.EnemyTargetHelper.OnTarget += FlipSpriteOnTarget;
        _baseUnit.ShamanTargetHelper.OnTarget += FlipSpriteOnTarget;
    }
    private void Update()
    {
        var position = (Vector2)transform.position;
        var deltaV = position - _lastPos;

        if (deltaV.sqrMagnitude >= 0.1f) //flip sprite according to movement
        {
            FlipX(deltaV.x >= 0);
            _lastPos = position;
        }
        
    }
    protected virtual void FlipSpriteOnTarget(BaseUnit target)
    {
        var distance = _baseUnit.transform.position - target.transform.position;
        FlipX(distance.x < 0);
    }

    private void FlipX(bool doFlip)
    {
        var localScale = transform.localScale;
        var scale = new Vector3()
        {
            x = doFlip ? -1 : 1,
            y = localScale.y,
            z = localScale.z,
        };
        localScale = scale;
        transform.localScale = localScale;
        OnSpriteFlip?.Invoke(doFlip);
    }

    protected abstract void OnUnitDeath();


    private void OnBecameVisible()
    {
        //send message to indicator sys
    }

    private void OnBecameInvisible()
    {
        //send message to indicator sys

    }
}
