using System;
using UnityEngine;


[Serializable]
public class WindEffectHandler : EffectTransitionLerp<WindParticleAnimationType>
{
    private ParticleSystem[] _windParticles;

    public void Init(ParticleSystem[] windParticles)
    {
        _windParticles = windParticles;
    }

    protected override void SetValue(WindParticleAnimationType type, float value)
    {
        switch (type)
        {
            case WindParticleAnimationType.SimulationSpeed:
                foreach (var particle in _windParticles)
                {
                    var main = particle.main;
                    main.simulationSpeed = value;
                }

                break;
        }
    }
}

public enum WindParticleAnimationType
{
    SimulationSpeed,
}