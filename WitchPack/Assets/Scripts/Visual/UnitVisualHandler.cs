using System;
using UnityEngine;

public class UnitVisualHandler : MonoBehaviour
{
    [HideInInspector,SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector2 _lastPos;
    private BaseUnit _baseUnit;
    private void OnValidate()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
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
    
    public void SpriteFlipX(bool doFlip)
    {
        spriteRenderer.flipX = doFlip;
        //_silhouette.flipX = doFlip;
        //OnSpriteFlipX?.Invoke(doFlip);
    }
}
