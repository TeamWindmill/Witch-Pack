using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingStoneMono : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polygonCollider;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    
    private OrbitalStonesMono _orbitalStones;
    private float _lerpTimer;
    private bool _isActive;

    public void Init(OrbitalStonesMono orbitalStones,int index, float timeToSpawn)
    {
        gameObject.SetActive(false);
        _spriteRenderer.sprite = _sprites[index];
        ResetCollider();
        _orbitalStones = orbitalStones;
        TimerManager.AddTimer(timeToSpawn, Activate, true);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        _isActive = true;
        TimerManager.AddTimer(_orbitalStones.Ability.GetAbilityStatValue(AbilityStatType.Duration), Disable,true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_isActive) return;
        Enemy target = other.GetComponent<Enemy>() ?? other.GetComponentInParent<Enemy>();
        if (!ReferenceEquals(target, null))
        {
            target.Damageable.GetHit(_orbitalStones.Owner.DamageDealer,_orbitalStones.Ability);
            target.DamageDealer.OnHitTarget += DisableStatusEffects;
            //Disable();
        }
    }

    private void DisableStatusEffects(Damageable arg1, DamageDealer damageDealer, DamageHandler arg3, Ability arg4, bool arg5)
    {
        damageDealer.Owner.Effectable.RemoveEffectsOfType(StatusEffectVisual.Weak);
        damageDealer.OnHitTarget -= DisableStatusEffects;
    }

    public void Disable()
    {
        _isActive = false;
        gameObject.SetActive(false);
        _orbitalStones.OnStoneDisable();
    }

    [Button]
    public void ResetCollider()
    {
        for (int i = 0; i < _polygonCollider.pathCount; i++) _polygonCollider.SetPath(i, new List<Vector2>());
        _polygonCollider.pathCount = _spriteRenderer.sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < _polygonCollider.pathCount; i++) {
            path.Clear();
            _spriteRenderer.sprite.GetPhysicsShape(i, path);
            _polygonCollider.SetPath(i, path.ToArray());
        }
    }

    private void OnValidate()
    {
        _polygonCollider ??= GetComponent<PolygonCollider2D>();
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }
}