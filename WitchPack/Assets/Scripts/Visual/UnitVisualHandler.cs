using System;
using UnityEngine;

public class UnitVisualHandler : MonoBehaviour
{
    public Action<bool> OnSpriteFlip;
    
    [HideInInspector,SerializeField] private SpriteRenderer spriteRenderer;
    [HideInInspector,SerializeField] private Animator unitAnimator;
    public Animator UnitAnimator => unitAnimator;


    private Vector2 _lastPos;
    private BaseUnit _baseUnit;
    private void OnValidate()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        unitAnimator ??= GetComponent<Animator>();
    }

    public void Init(BaseUnit unit, BaseUnitConfig config)
    {
        _baseUnit = unit;
        spriteRenderer.sprite = config.UnitSprite;
    }

    private void Update()
    {
        var position = (Vector2)transform.position;
        var deltaV = position - _lastPos;

        if (deltaV.sqrMagnitude >= 0.1f)
        {
            SpriteFlipX(deltaV.x >= 0);
            _lastPos = position;
        }
        
        //if unit has target
        {
            //var targetDelta = position.x - target.transform.position.x;
            //SetSpriteFlipX(targetDelta < 0);
        }
    }
    

    private void SpriteFlipX(bool doFlip)
    {
        spriteRenderer.flipX = doFlip;
        OnSpriteFlip?.Invoke(doFlip);
        //_silhouette.flipX = doFlip;
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
