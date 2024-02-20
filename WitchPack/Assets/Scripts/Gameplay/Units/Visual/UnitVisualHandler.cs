using System;
using UnityEngine;

public class UnitVisualHandler : MonoBehaviour
{
    public Action<bool> OnSpriteFlip;
    public Animator Animator => animator;
    public UnitEffectHandler EffectHandler => effectHandler;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private UnitAnimator unitAnimator;
    [SerializeField] private UnitEffectHandler effectHandler;


    private Vector2 _lastPos;
    private BaseUnit _baseUnit;

    private void Awake()
    {
        unitAnimator.OnDeathAnimationEnd += ResetSprite;
    }

    public virtual void Init(BaseUnit unit, BaseUnitConfig config)
    {
        _baseUnit = unit;
        spriteRenderer.sprite = config.UnitSprite;
        _baseUnit.EnemyTargetHelper.OnTarget += FlipSpriteOnTarget;
        _baseUnit.ShamanTargetHelper.OnTarget += FlipSpriteOnTarget;
    }
    private void Update()
    {
        var position = (Vector2)transform.position;
        var deltaV = position - _lastPos;

        if (deltaV.sqrMagnitude >= 0.1f) //flip sprite according to movement
        {
            SpriteFlipX(deltaV.x >= 0);
            _lastPos = position;
        }
        
    }
    protected virtual void FlipSpriteOnTarget(BaseUnit target)
    {
        var distance = _baseUnit.transform.position - target.transform.position;
        SpriteFlipX(distance.x < 0);
    }

    protected void SpriteFlipX(bool doFlip)
    {
        spriteRenderer.flipX = doFlip;
        OnSpriteFlip?.Invoke(doFlip);
        //_silhouette.flipX = doFlip;
    }

    private void ResetSprite()
    {
        Color color = Color.white;
        color.a = 1;
        spriteRenderer.color = color;
        spriteRenderer.transform.localScale = Vector3.one;
    }

    private void OnBecameVisible()
    {
        //send message to indicator sys
    }

    private void OnBecameInvisible()
    {
        //send message to indicator sys

    }



}
