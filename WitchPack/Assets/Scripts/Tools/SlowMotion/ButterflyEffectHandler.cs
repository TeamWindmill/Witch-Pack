using System;
using UnityEngine;

[Serializable]
public class ButterflyEffectHandler : EffectTransitionLerp<ParticleAnimationType>
{
    private ParticleSystem[] _butterfliesParticles;

    public void Init(ParticleSystem[] windParticles)
    {
        _butterfliesParticles = windParticles;
    }

    protected override void SetValue(ParticleAnimationType type, float value)
    {
        switch (type)
        {
            case ParticleAnimationType.SimulationSpeed:
                foreach (var particle in _butterfliesParticles)
                {
                    var main = particle.main;
                    main.simulationSpeed = value;
                }

                break;
        }
    }
}