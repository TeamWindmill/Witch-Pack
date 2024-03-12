
using UnityEngine;

public class ShamanVisualHandler : UnitVisualHandler
{
    [SerializeField] private ParticleSystem healEffect;
    [SerializeField] private ParticleSystem overhealEffect;
    [SerializeField] private ParticleSystem healingWeedsEffect;
    [SerializeField] private Transform _rangeVisual;


    public ParticleSystem HealEffect { get => healEffect; }
    public ParticleSystem OverhealEffect { get => overhealEffect; }
    public ParticleSystem HealingWeedsEffect { get => healingWeedsEffect; }

    public override void Init(BaseUnit unit, BaseUnitConfig config)
    {
        _rangeVisual.gameObject.SetActive(false);
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
}