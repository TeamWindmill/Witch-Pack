
using Configs;
using UnityEngine;

namespace Gameplay.Units.Visual
{
    public class ShamanVisualHandler : UnitVisualHandler
    {
        [SerializeField] private ParticleSystem healEffect;
        [SerializeField] private ParticleSystem overhealEffect;
        [SerializeField] private ParticleSystem healingWeedsEffect;
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private ParticleSystem _selectionRing;
        [SerializeField] private Transform _rangeVisual;
        public ShamanEffectHandler ShamanEffectHandler => EffectHandler as ShamanEffectHandler;


        public ParticleSystem HealEffect { get => healEffect; }
        public ParticleSystem OverhealEffect { get => overhealEffect; }
        public ParticleSystem HealingWeedsEffect { get => healingWeedsEffect; }
        public ParticleSystem HitEffect => _hitEffect;

        public override void Init(BaseUnit unit, BaseUnitConfig config)
        {
            _rangeVisual.gameObject.SetActive(false);
            _selectionRing.gameObject.SetActive(false);
            base.Init(unit, config);
        }

        protected override void OnUnitDeath()
        {
            _baseUnit.gameObject.SetActive(false);
        }
    
        public void ShowShamanRange()
        {
            _rangeVisual.gameObject.SetActive(true);
        }
    
        public void HideShamanRange()
        {
            _rangeVisual.gameObject.SetActive(false);
        }
        public void ShowShamanRing()
        {
            _selectionRing.gameObject.SetActive(true);
        }
        public void HideShamanRing()
        {
            _selectionRing.gameObject.SetActive(false);
        }
    }
}