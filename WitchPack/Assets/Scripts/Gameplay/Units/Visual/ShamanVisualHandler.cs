
using UnityEngine;

public class ShamanVisualHandler : UnitVisualHandler
{
    [SerializeField] private ParticleSystem healEffect;
    [SerializeField] private ParticleSystem overhealEffect;


    public ParticleSystem HealEffect { get => healEffect; }
    public ParticleSystem OverhealEffect { get => overhealEffect; }

    protected override void OnUnitDeath()
    {
        _baseUnit.gameObject.SetActive(false);
    }
}