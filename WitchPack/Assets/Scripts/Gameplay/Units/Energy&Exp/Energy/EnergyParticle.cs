using DG.Tweening;
using Gameplay.Pools.Pool_System;
using Gameplay.Targeter;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    public class EnergyParticle : MonoBehaviour, IPoolable
    {
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private ShamanTargeter particleCollider;
        [SerializeField] private float magnetSpeed;
        [SerializeField] private float dropDuration;
        [SerializeField] private float dropRadius;
        [SerializeField] private Ease dropAnimationEase;
        private int _energyValue;
        private bool _shamanInRadius;
        private Shaman _shamanTarget;

        private void Awake()
        {
            particleCollider.OnTargetAdded += PopEnergyParticle;
        }

        public void Init(Vector3 position,int energyValue,int randomAngle)
        {
            _energyValue = energyValue;
            transform.position = position;
            gameObject.SetActive(true);
            ParticleDropAnimation(position,randomAngle);
        }

        private void ParticleDropAnimation(Vector3 position,int randomAngle)
        {
            var x = Mathf.Sin(randomAngle);
            var y = Mathf.Cos(randomAngle);
            transform.DOMove(position + new Vector3(x, y, 0) * dropRadius, dropDuration).SetEase(dropAnimationEase);
        }

        private void PopEnergyParticle(Shaman shaman)
        {
            PartyEnergyHandler.AddEnergy(_energyValue);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_shamanInRadius) return;
            if (other.TryGetComponent<Shaman>(out var shaman))
            {
                _shamanInRadius = true;
                _shamanTarget = shaman;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Shaman>(out var shaman))
            {
                if (shaman != _shamanTarget) return;
                _shamanInRadius = false;
                _shamanTarget = null;
            }
        }

        private void Update()
        {
            if (_shamanInRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, _shamanTarget.transform.position, magnetSpeed * Time.deltaTime);
            }
        }

        public GameObject PoolableGameObject => gameObject;
    }
}
