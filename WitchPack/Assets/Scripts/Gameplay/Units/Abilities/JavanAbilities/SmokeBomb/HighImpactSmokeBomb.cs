
using System;
using System.Collections;
using UnityEngine;

public class HighImpactSmokeBomb : SmokeBomb
{
    [SerializeField] private ParticleSystem _explosionParticleSystem;
    private bool _explosionActive = true;
    private float _explosionTimer;
    private const float _explosionTime = 0.5f;
    
    
    public override void SpawnBomb(SmokeBombSO config, BaseUnit owner)
    {
        base.SpawnBomb(config, owner);
        _explosionParticleSystem.gameObject.SetActive(true);
        StartCoroutine(ExplosionTimer());
    }

    protected override void OnTargetEntered(GroundCollider collider)
    {
        if (_explosionActive)
        {
            if (collider.Unit is Enemy enemy)
            {
                enemy.Damageable.GetHit(_owner.DamageDealer,_ability);
            }
        }
        base.OnTargetEntered(collider);
    }

    private IEnumerator ExplosionTimer()
    {
        while (_explosionActive)
        {
            _explosionTimer += GAME_TIME.GameDeltaTime;
            if (_explosionTimer >= _explosionTime) _explosionActive = false;
            yield return new WaitForEndOfFrame();
        }
    }
}