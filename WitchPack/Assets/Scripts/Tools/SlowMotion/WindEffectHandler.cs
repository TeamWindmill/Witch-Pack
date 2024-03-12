using System;
using UnityEngine;


[Serializable]
public class WindEffectHandler : EffectTransitionLerp<ParticleAnimationType>
{
    private ParticleSystem[] _windParticles;

    public void Init(ParticleSystem[] windParticles)
    {
        _windParticles = windParticles;
    }

    protected override void SetValue(ParticleAnimationType type, float value)
    {
        switch (type)
        {
            case ParticleAnimationType.SimulationSpeed:
                foreach (var particle in _windParticles)
                {
                    var main = particle.main;
                    main.simulationSpeed = value;
                }

                break;
        }
    }
}

public enum ParticleAnimationType
{
    SimulationSpeed,
}