using System;
using DG.Tweening;
using Tools.Lerp;
using UnityEngine;

namespace Tools.SlowMotion
{
    [Serializable]
    public class WindEffectHandler 
    {
        private ParticleSystem[] _windParticles;
        [SerializeField] private LerpConfig<float> LerpConfig;
        public void Init(ParticleSystem[] windParticles)
        {
            _windParticles = windParticles;
        }
        public void StartTransition()
        {
        
            foreach (var particle in _windParticles)
            {
                var main = particle.main;
                DOTween.To(() => main.simulationSpeed, x => main.simulationSpeed = x, LerpConfig.EndValue, LerpConfig.TransitionTime);
            }
        }

        public void EndTransition()
        {
            foreach (var particle in _windParticles)
            {
                var main = particle.main;
                DOTween.To(() => main.simulationSpeed, x => main.simulationSpeed = x, LerpConfig.StartValue, LerpConfig.TransitionTime);
            }
        }
    }
}