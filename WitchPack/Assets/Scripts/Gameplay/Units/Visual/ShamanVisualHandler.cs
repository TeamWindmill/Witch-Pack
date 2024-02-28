
using UnityEngine;

public class ShamanVisualHandler : UnitVisualHandler
{
    [SerializeField] private ParticleSystem healEffect;
    [SerializeField] private ParticleSystem overhealEffect;
    [SerializeField] private ParticleSystem healingWeedsEffect;


    public ParticleSystem HealEffect { get => healEffect; }
    public ParticleSystem OverhealEffect { get => overhealEffect; }
    public ParticleSystem HealingWeedsEffect { get => healingWeedsEffect; }

    protected override void OnUnitDeath()
    {
        _baseUnit.gameObject.SetActive(false);
    }
}