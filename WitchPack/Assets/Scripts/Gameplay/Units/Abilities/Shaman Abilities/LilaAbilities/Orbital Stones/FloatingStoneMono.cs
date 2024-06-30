using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FloatingStoneMono : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private Sprite sprite;
    
    
    private OrbitalStonesMono _orbitalStones;
    [Unity.Collections.ReadOnly] public float _currentAngle;
    private float _lerpTimer;
    private bool _isActive;
    //private Action _spawnNextStone;
    //private float _angleDiff;

    public void Init(OrbitalStonesMono orbitalStones)
    {
        _orbitalStones = orbitalStones;
        _currentAngle = 0;
        SetCirclePos();
        //_spawnNextStone = spawnNextStone;
        //_angleDiff = angleDiff;
        _isActive = true;
        gameObject.SetActive(true);

        TimerManager.AddTimer(orbitalStones.Ability.GetAbilityStatValue(AbilityStatType.Duration), Disable);
    }

    private void FixedUpdate()
    {
        //rotation
        if(!_isActive) return;
        // if (_currentAngle >= _angleDiff)
        // {
        //     _spawnNextStone?.Invoke();
        //     _spawnNextStone = null;
        // }
        if (_currentAngle >= 360) _currentAngle = 0;
        _currentAngle += _orbitalStones.AngularSpeed * Time.fixedDeltaTime;
                                                      
        SetCirclePos();
    }

    private void SetCirclePos()
    {
        var posX = MathF.Sin(_currentAngle * Mathf.Deg2Rad);             
        var posY = MathF.Cos(_currentAngle * Mathf.Deg2Rad)/_orbitalStones.EllipseScale;
        var offset = new Vector3(posX,posY ,0) * _orbitalStones.Radius;
        transform.position = _orbitalStones.transform.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        damageDealer.Owner.Effectable.RemoveEffectsOfType(StatusEffectType.Weak);
        damageDealer.OnHitTarget -= DisableStatusEffects;
    }

    public void Disable()
    {
        _isActive = false;
        gameObject.SetActive(false);
        // _spawnNextStone?.Invoke();
        // _spawnNextStone = null;
        _orbitalStones.OnStoneDisable();
    }

    [Button]
    public void ResetCollider()
    {
        for (int i = 0; i < polygonCollider.pathCount; i++) polygonCollider.SetPath(i, new List<Vector2>());
        polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++) {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }
    }

    private void OnValidate()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
}