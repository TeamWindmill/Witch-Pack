using System.Collections;
using Gameplay.Pools.Pool_System;
using Gameplay.Targeter;
using GameTime;
using UnityEngine;
using UnityEngine.Playables;

namespace Gameplay.Units.Abilities.Shaman_Abilities.JavanAbilities.SmokeBomb
{
    public class HighImpactSmokeBombMono : SmokeBombMono, IPoolable
    {
        [SerializeField] private ParticleSystem _explosionParticleSystem;
        private bool _explosionActive = true;
        private float _explosionTimer = 0;
        private const float _explosionTime = 1;
    
    
        public override void SpawnBomb(SmokeBomb ability, BaseUnit owner)
        {
            base.SpawnBomb(ability, owner);
            _explosionParticleSystem.gameObject.SetActive(true);
            _explosionTimer = 0;
            _explosionActive = true;
            StartCoroutine(ExplosionTimer());
        }

        protected override void OnTargetEntered(GroundCollider collider)
        {
            if (_explosionActive)
            {
                if (collider.Unit is Enemy.Enemy enemy)
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

        protected override void OnEnd(PlayableDirector director)
        {
            _explosionParticleSystem.gameObject.SetActive(false);
            base.OnEnd(director);
        }
        public GameObject PoolableGameObject => gameObject;
    }
}