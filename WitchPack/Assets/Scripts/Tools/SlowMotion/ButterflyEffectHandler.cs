using System;
using DG.Tweening;
using Tools.Lerp;
using UnityEngine;

namespace Tools.SlowMotion
{
    [Serializable]
    public class ButterflyEffectHandler
    {
        private ParticleSystem[] _butterfliesParticles;
        [SerializeField] private LerpConfig<float> LerpConfig;

        public void Init(ParticleSystem[] windParticles)
        {
            _butterfliesParticles = windParticles;
        }

        public void StartTransition()
        {
            foreach (var particle in _butterfliesParticles)
            {
                var main = particle.main;
                DOTween.To(() => main.simulationSpeed, x => main.simulationSpeed = x, LerpConfig.EndValue, LerpConfig.TransitionTime);
            }
        
        }

        public void EndTransition()
        {
            foreach (var particle in _butterfliesParticles)
            {
                var main = particle.main;
                DOTween.To(() => main.simulationSpeed, x => main.simulationSpeed = x, LerpConfig.StartValue, LerpConfig.TransitionTime);
            }
        }

        public float CurrentValue
        {
            get => CurrentValue;
            set
            {
                //SetValue(value);
                CurrentValue = value;
            }
        }

    
    }
}