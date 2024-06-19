
using UnityEngine;

public class RockMonolithSO : OffensiveAbilitySO
{
    [SerializeField] private float _duration;
    [SerializeField] private StatusEffectConfig _movementSpeedSlow;
    [SerializeField] private StatusEffectConfig _armorGain;
    [SerializeField] private float _tauntRadius;

    public float Duration => _duration;
    public StatusEffectConfig MovementSpeedSlow => _movementSpeedSlow;
    public StatusEffectConfig ArmorGain => _armorGain;
    public float TauntRadius => _tauntRadius;
}