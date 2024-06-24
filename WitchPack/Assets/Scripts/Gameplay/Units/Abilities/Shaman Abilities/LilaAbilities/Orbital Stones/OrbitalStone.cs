using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class OrbitalStone : MonoBehaviour
{
    [ReadOnly] public float _currentAngle;
    private float _lerpTimer;
    private float _posX, _posY;
    private bool _isActive;
    private OrbitalStonesMono _orbitalStones;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private Sprite sprite;

    public void Init(OrbitalStonesMono orbitalStones, float startAngle)
    {
        _orbitalStones = orbitalStones;
        _currentAngle = startAngle;
        _isActive = true;
    }

    private void Update()
    {
        //rotation
        if(!_isActive) return;                                        
        if (_currentAngle >= 360) _currentAngle = 0;
        _currentAngle += _orbitalStones.AngularSpeed * Time.deltaTime;
                                                      
        _posX = MathF.Sin(_currentAngle * Mathf.Deg2Rad);             
        _posY = MathF.Cos(_currentAngle * Mathf.Deg2Rad)/_orbitalStones.EllipseScale;
        var offset = new Vector3(_posX,_posY ,0) * _orbitalStones.Radius;
        transform.position = _orbitalStones.transform.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy target = other.GetComponent<Enemy>() ?? other.GetComponentInParent<Enemy>();
        if (!ReferenceEquals(target, null))
        {
            target.Damageable.GetHit(_orbitalStones.Owner.DamageDealer,_orbitalStones.Ability);
            Disable();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

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
        ResetCollider();
    }
}