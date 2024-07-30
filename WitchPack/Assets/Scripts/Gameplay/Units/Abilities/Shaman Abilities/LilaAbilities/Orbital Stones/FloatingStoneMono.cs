using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public class FloatingStoneMono : MonoBehaviour, IPoolable
{
    [SerializeField] private PolygonCollider2D _polygonCollider;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;


    private OrbitalStonesMono _orbitalStones;
    [Unity.Collections.ReadOnly] public float _currentAngle;
    private float _lerpTimer;
    private bool _isActive;

    public void Init(OrbitalStonesMono orbitalStones,int index)
    {
        var spriteIndex = index >= _sprites.Length ? index - (_sprites.Length - 1) : index;
        _spriteRenderer.sprite = _sprites[spriteIndex];
        ResetCollider();
        _orbitalStones = orbitalStones;
        _currentAngle = 0;
        SetCirclePos();
        _isActive = true;
        gameObject.SetActive(true);

        TimerManager.AddTimer(orbitalStones.Ability.GetAbilityStatValue(AbilityStatType.Duration), Disable,true);
    }

    private void FixedUpdate()
    {
        //movement
        if(!_isActive) return;

        if (_currentAngle >= 360) _currentAngle = 0;
        _currentAngle += _orbitalStones.AngularSpeed * GAME_TIME.GameFixedDeltaTime;
        SetCirclePos();

        //rotation
        // vector from this object towards the target location
        Vector3 vectorToTarget = _orbitalStones.transform.position - transform.position;
         
        //rotate that vector by 90 degrees around the Z axis
        transform.rotation = Quaternion.LookRotation(Vector3.forward,vectorToTarget);


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

    public GameObject PoolableGameObject => gameObject;
}